$(function () {
    $('.btn-secondary').on('click', function () {

        var selectedCategory = this.textContent;
        console.log("Selected category: " + selectedCategory);
        var cat = this.getAttribute('value');
        var selectedCatContainer = $('#category-container' + cat);

        if (selectedCatContainer.children().length == 0) {
            //If we haven't got any content inside the category div, retrieve the content trough ajax.

            var partial = '/Orders/Edit?handler=Category&categoryId=' + cat;

            selectedCatContainer.load(partial, function () {
                //Execute when request has been completed.
                console.log("Obtained \"" + selectedCategory + "\" category trough ajax.");
                showCategory(selectedCatContainer);
            });

        } else {
            showCategory(selectedCatContainer);
        }
    });
});

function showCategory(selectedCategory) {
    var categoryTabs = $('.category-content');

    //Hide the other (all) categories
    for (var i = 0; i < categoryTabs.length; i++) {
        categoryTabs[i].hidden = true;
    }
    //Make sure the selected category is visible.
    selectedCategory.removeAttr('hidden');
}

//Adds item to the order.
function AddItem(id) {
    var itemcontainer = $("#itemcontainer");
    var index = document.URL.lastIndexOf('=');
    var orderid = document.URL.substring(index + 1);
    console.log(orderid);
    var partial = '/Shared/OrderItems/AddItem?orderId=' + orderid + '&productId=' + id;

    itemcontainer.load(partial, function () {
        console.log("Added product with id: " + id);
    });
}

//Removes item
function RemoveItem(id) {
    var itemcontainer = $("#itemcontainer");
    var index = document.URL.lastIndexOf('=');
    var orderid = document.URL.substring(index + 1);
    console.log(orderid);
    var partial = '/Orders/Edit?handler=RemoveItem&orderId=' + orderid + '&itemId=' + id;

    itemcontainer.load(partial, function () {
        console.log("Removed product with id: " + id);
    });
}

function IncreaseItem(id) {

}

function DecreaseItem(id) {

}