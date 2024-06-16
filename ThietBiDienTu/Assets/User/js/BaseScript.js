var common = {
    init function() {
        common.registerEvent();
    },
    registerEvent: function () {

        $("#txttimkiem").autocomplete({
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/SanPham/ListSanPham",
                    dataType: "json",
                    data: {
                        term: request.term
                    },
                    success: function (data) {
                        response(data);
                    }
                });
            },
            focus: function (event, ui) {
                $("#txttimkiem").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txttimkiem").val(ui.item.label);
                return false;
            }
        })
            .autocomplete("instance")._renderItem = function (ul, item) {
                return $("<li>")
                    .append("<div>" + item.label + "<br>" + item.desc + "</div>")
                    .appendTo(ul);
            };
    }
}
common.init();