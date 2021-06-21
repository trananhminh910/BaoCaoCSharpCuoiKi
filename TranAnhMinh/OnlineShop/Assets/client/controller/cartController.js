var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = '/Customer/Home/';
        });
        //payment
        $('#btnPayment').off('click').on('click', function () {
            window.location.href = '/Customer/cart/payment';
        });

        //update quantity
        //btnMoreQuantity
        $('.btnMoreQuantity').off('click').on('click', function () {
            var listProduct = $('.txtQuantity');
            var cartList = [];
            var itemID = parseInt($(this).prev().data('id'));  
            var quantity = parseInt($(this).prev().text()) +1;  
            cartList.push({
                Quantity: quantity,
                Product: {
                    ID: itemID
                }
            });
            $.ajax({
                url: '/Customer/Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Customer/cart";
                    }
                }
            })
        });
        //btnLessQuantity
        $('.btnLessQuantity').off('click').on('click', function () {
            var listProduct = $('.txtQuantity');
            var cartList = [];
            var itemID = parseInt($(this).next().data('id'));
            var quantity = parseInt($(this).next().text()) - 1;
            cartList.push({
                Quantity: quantity,
                Product: {
                    ID: itemID
                }
            });
            $.ajax({
                url: '/Customer/Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Customer/cart";
                    }
                }
            })
        });
        //delete all
        $('#btnDeleteAll').off('click').on('click', function () {

            $.ajax({
                url: '/Customer/Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Customer/cart";
                    }
                }
            })
        });

        //delete a item
        $('.btn-delete').off('click').on('click', function () {

            $.ajax({
                data: {id:$(this).data('id')},
                url: '/Customer/Cart/Delete',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Customer/cart";
                    }
                }
            })
        });
    }
}
cart.init();