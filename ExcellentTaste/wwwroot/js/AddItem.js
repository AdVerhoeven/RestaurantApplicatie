//Adds item to the order.
function AddItem(id) {
    var itemcontainer = $("#itemcontainer");
    var index = document.URL.lastIndexOf('=');
    var orderid = document.URL.substring(index + 1);
    console.log(orderid);
    var partial = '/Orders/Edit?handler=AddItem&orderId=' + orderid + '&productId=' + id;

    itemcontainer.load(partial, function () {
        console.log("Added product with id: " + id);
    });
}