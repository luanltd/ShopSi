﻿var user = {
    init: function () {
        user.registerEvent();
    },
    registerEvent: function () {
        $(".xoa").off('click').on('click', function () {
            var id=$(this).data('id');
            $.ajax({
                url: "/User/Delete",
                data: { id: id },
                type: "POST",
                dataType:"json",
                success:function(res){
                    if (res.status) {
                        window.location.href("/Admin/User/Index"),
                        alert("Xóa thành công")
                    }
            }
            })
        })
    }
};
user.init();
