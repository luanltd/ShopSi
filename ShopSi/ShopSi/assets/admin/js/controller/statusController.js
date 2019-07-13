var status = {
    init: function () {
        status.registerEvent();
    },
    registerEvent: function () {
        $('.btn_active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/User/ChangeStatus",
                data: {id: id},
                type: "POST",
                dataType: "json",
                success: function (res) {
                    console.log(res);
                    if (res.status) {
                        btn.text('Kích Hoạt');
                    }
                    else {
                        btn.text('Khóa');
                    }
                }
            });
        });
    }
}
status.init();