var ckfinder = {
    init: function () {
        ckfinder.registerEvent();
    },
    registerEvent: function () {
        $('#btnSelectImage').off('click').on('click', function (e) {
            e.preventDefault();
            var finder = new CKFinder();
            finder.selectActionFunction = function (url) {
                $('#Image').val(url);
            };
            finder.popup();

        })
    }

}
ckfinder.init();