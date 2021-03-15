﻿$(function () {
    $('.btn-secondary').on('click', function () {
        var selectedCategory = this.textContent;
        console.log("Selected category: " + selectedCategory);
        var cat = this.getAttribute('value');
        var selectedCatContainer = $('#category-container' + cat);

        if (selectedCatContainer.children().length == 0) {
            //If we haven't got any content inside the category div, retrieve the content trough ajax.
            var index = document.URL.lastIndexOf('=');
            var order = document.URL.substring(index);
            var partial = '/Orders/Edit?handler=NewItems&orderId=' + order + '&categoryId=' + cat;

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

    for (var i = 0; i < categoryTabs.length; i++) {
        categoryTabs[i].hidden = true;
    }
    selectedCategory.removeAttr('hidden');
}