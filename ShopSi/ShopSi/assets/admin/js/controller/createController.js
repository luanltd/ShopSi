var create = {
    init: function () {
        create.loadProvince();
        create.registerEvent();
    },
    registerEvent: function () {
        $('#ddlProvince').off('change').on('change', function () {
            var id = $(this).val();
            if (id != '') {
                create.loadDistrict(parseInt(id));
            }
            else {
                $('#ddlDistrict').html('');
            }

        })
    },
    loadProvince: function () {
      
        $.ajax({
            url: "/Admin/User/LoadProvince",
            type: "POST",
            dataType: "json",
            success: function (res) {
                if (res.status) {
                    var data = res.data;
                 
                    var html = '<option value="">--Chọn tỉnh thành--</option>';
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.ID + '">' + item.Name + '</option>'
                    });
                    $('#ddlProvince').html(html);
                }
            }

        })

    },

    loadDistrict: function (id) {
       
        $.ajax({
            url: "/Admin/User/LoadDistrict",
            type: "POST",
            data: { provinceID: id },
            dataType: "json",
            success: function (res) {
                if (res.status) {
                    var data = res.data;
                    var html = '<option value="">--Chọn quận huyện--</option>';
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.ID + '">' + item.Name + '</option>'
                    });
                    $('#ddlDistrict').html(html);
                }
            }

        })
    }
}
create.init();