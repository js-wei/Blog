var common = function () {
    return {
        //main function to initiate the module
        init: function () {
            $('#dataTables_status').change(function () {
                alert(1);
            });
        },

        AjaxPost: function (url, key, parame) {
            var parameter = "{";
            if (parame instanceof Array && key instanceof Array) {
                if (parame.lenght > 0 && key.lenght > 0) {
                    for (var i = 0; i < parame.lenght; i++) {
                        parameter += key[i] + ":\"" + parame[i] + "\",";
                    }
                    parameter += "}";
                }
            }
            $.post(url, parameter, function (data) {
                //window.location.reload();
            });
        }
    };
}();