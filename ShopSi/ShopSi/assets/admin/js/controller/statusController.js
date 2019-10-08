var status1 = {
    init1: function () {
        status1.registerEvent1();
    },
    registerEvent1: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/User/ChangeStatus",
                data: { id: id },
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
};

status1.init1();
