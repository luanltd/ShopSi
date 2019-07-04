var commom = {
    init: function () {
        commom.registerEvent();
    },
    registerEvent: function () {
        $( "#txtSearching" ).autocomplete({
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/Home/ListName",
                    dataType: "json",
                    data: {
                        q: request.term
                    },
                    success: function (res) {
                        response(res.data);
                    }
                });
            },
            focus: function( event, ui ) {
                $( "#txtSearching" ).val( ui.item.label );
                return false;
            },
            select: function( event, ui ) {
                $( "#txtSearching" ).val( ui.item.label );              
                return false;
            }
        })
        .autocomplete( "instance" )._renderItem = function( ul, item ) {
       return $( "<li>" )
      .append( "<a>" + item.label +"</a>" )
      .appendTo( ul );
};   
    }
}
commom.init();