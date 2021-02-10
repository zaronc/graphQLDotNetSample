$(document).ready(function(){
    getSuppliers();
})

function getSuppliers() {
    queryFetch(`
        query {
            suppliers {
                name
                id
            }
        }`
    ).then(data=>{
        var select = $("#selectSuppliers");
        data.data.suppliers.forEach(fe=> {
            select.append(`<option value=${fe.id}>${fe.name}</option>`);    
        })
    });
}

function getProductsBySupplier(supplierId) {
    var table = $("#produtcsQuery1");
    queryFetch(`
        query productsBySupplierId{
          productsBySupplier(id:${parseInt(supplierId)}){
            id
            name
            category
            prezzo            
          }
        }
    `).then(data=>{
        table.html("");
        data.data.productsBySupplier.forEach(fe=>{
            table.append(`<tr style="cursor: pointer" onclick="getReviewsByProductId(${fe.id})"><td>${fe.name}</td><td>${fe.prezzo} €</td><td>${fe.category}</td></tr>`)
        })
    });
}

function getReviewsByProductId(productId) {
    $("#mutProductId").val(productId);
    selectedProduct = productId;
    var divReviews = $("#reviewsCards");
    queryFetch(`
        query reviewsByProductId{
          reviewsByProduct(id:${parseInt(productId)}){
            authorName
            comment
            rate            
          }
        }
    `).then(data=>{
        divReviews.html("");
        data.data.reviewsByProduct.forEach(fe=>{
            divReviews.append(`
                <div class="col-3 pb-1">
                    <div class="card" style="width: 18rem;">
                      <div class="card-body">
                        <h5 class="card-title">Rate: ${fe.rate}</h5>
                        <h6 class="card-subtitle mb-2 text-muted fst-italic">${fe.authorName}</h6>
                        <p class="card-text">${fe.comment}</p>
                        
                      </div>
                    </div>
                </div>
            `);    
        });
        
    });
}

function queryFetch(query) {
    return $.ajax({
        url: 'http://localhost:5000/graphql',
        headers: { 'Content-Type': 'application/json' },
        type: 'POST',
        data: JSON.stringify({
            query: query
        })
    });
}

function createReview(form) {
    if(form.elements["mutProductId"].value == "") {
        window.alert("Product not selected!")
        return;
    }
    
    var author = form.elements["author"].value;
    var rate = form.elements["rate"].value;
    var comment = form.elements["comment"].value;
    var productId = form.elements["mutProductId"].value
    queryMutation(`
        mutation creazioneRecensione($rev: ReviewInput!){
          createReview(reviewInput: $rev){
            authorName
            comment
            rate
            productId
          }
        }
    `, {
        rev: {
            authorName: author,
            rate: rate,
            comment: comment,
            productId: productId
        }
    }).then(window.alert("Review added!"));
}

function queryMutation(query, variables){
    return $.ajax({
        url: 'http://localhost:5000/graphql',
        headers: { 'Content-Type': 'application/json' },
        type: 'POST',
        data: JSON.stringify({
            query: query,
            variables: variables
        })
    });
}

function subscribeToEvent(eventName){
    var sub = `subscription ${eventName} {
			  reviewAdded{
				authorName
				comment
				rate
				productId
			  }
			}`;
    const socket = new WebSocket("ws://localhost:5000/graphql", "graphql-ws");
    socket.onopen = function(e) {
        $("#subSuccess").html(`Success. This client is now subscribed to \"${eventName}\" event.`)
        console.log("Connessione stabilita, invio la subscription...");
        socket.send(JSON.stringify({type:"connection_init",payload:{}}));
        socket.send(JSON.stringify({id:"1",type:"start",payload:{query:sub}}));
    };
    socket.onmessage = function(event) {
        const data = JSON.parse(event.data);
        if(data.type == "connection_ack" ) return;
        
        var review = data.payload.data.reviewAdded;
        
        var divReviews = $("#reviewsCards");
        divReviews.append(`
            <div class="col-3 pb-1">
                <div class="card" style="width: 18rem;">
                  <div class="card-body">
                    <h5 class="card-title">Rate: ${review.rate}</h5>
                    <h6 class="card-subtitle mb-2 text-muted fst-italic">${review.authorName}</h6>
                    <p class="card-text">${review.comment}</p>
                    
                  </div>
                </div>
            </div>
        `);
        
    }
}

function create(){
    var mut = `{\"operationName\":\"createRev\",\"variables\":{\"rev\":{\"authorName\":\"io\",\"rate\":4,\"comment\":\"ciao\"}},\"query\":\"mutation createRev($rev: ReviewInput!) {  createReview(reviewInput: $rev) {    authorName    comment    rate  }}\"}`;
    $.ajax({
        url: 'https://localhost:5001/graphql',
        type: 'POST',
        data: mut,
        headers: { 'Content-Type': 'application/json' },
        dataType: 'json',
        success: function (data) {
            var a = "";
        }
    });
}


function GetProductsREST(){
    var starTime= new Date().getTime();
    $.ajax({
        url: 'https://localhost:5002/api/product',
        type: 'GET',
        success: function (data) {
            var timing = new Date().getTime()-starTime;

            $("#result").html( timing +'ms');
        }
    });
}

var selectedProduct;