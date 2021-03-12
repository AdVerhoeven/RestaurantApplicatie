$(function () {
    $('.btn-secondary').on('click', function () {
        console.log(this.textContent);
        var cat = this.getAttribute('value');
        var order = document.URL[document.URL.length-1];
        var partial = '/Orders/Edit?handler=NewItems&orderId=' + order + '&categoryId=' + cat;
        console.log(partial);
        //TODO: proper fix
        $(".category-content").textContent = "";
        $(".category-content").load(partial, function () {
            console.log("Performed load.")
        });

        
    });
});