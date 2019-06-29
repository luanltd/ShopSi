var cart = {
    init: function () {
        cart.registerEvent();
    },
    registerEvent: function () {
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/";
        });

        $('.deleteID').off('click').on('click', function () {
            var id = $(this).data('id');
            $.ajax({
                url: "/Cart/Delete",
                data: {
                    id: id
                },
                type: "POST",
                dataType: "json",
                success: function (res) {
                    if (res.status) {
                        window.location.href = "/gio-hang"
                    }
                }
            })
        });

        $('#btnUpdate').off('click').on('click', function () {
            var quantityPro = $('.txtquantity');
            var cartList = [];
            $.each(quantityPro, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Product: {
                        ID: $(item).data('id')
                    }
                })
            });
            $.ajax({
                url: "/Cart/Update",
                data: {
                    cartModel: JSON.stringify(cartList)
                },
                type: "POST",
                dataType: "json",
                success: function (res) {
                    if (res.status) {
                        window.location.href = "/gio-hang"
                    }
                }

            })
        });

        $('#btnDeleteAll').off('click').on('click', function () {
            $.ajax({
                url: "/Cart/DeleteAll",
                type: "POST",
                dataType: "json",
                success: function (res) {
                    if (res.status) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });

        $('#btnPayment').off('click').on('click', function () {
            window.location.href = "/thanh-toan";
        })
    }

}
cart.init();