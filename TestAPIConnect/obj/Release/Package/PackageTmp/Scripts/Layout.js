var myApp = { appUrl : ''};
$(document).ajaxError(function (xhr, props) {
    if (props.status == 403) {
        ////hiển thị modal login
        $("#login-modal").modal('show');
        setTimeout(function () { $('#userId').focus(); }, 700);
    }
});

function doAjaxLogin() {

    $.ajax({
        type: "POST",
        url: myApp.appUrl + '/Account/DoAjaxLogin',
        data: { userName: $('#userId').val(), password: $('#password').val() },
        beforeSend: function () {
            blockUI();
        },
        success: function (rs) {
            unBlockUI();
            if (rs.rs == 0) {
                $('#userId').focus() ;
                swal({
                    title: "Thông báo",
                    text: rs.error,
                    type: 'info',
                    confirmButtonClass: 'btn btn-confirm mt-2'
                });
            }
            else {
                $("#login-modal").modal('hide');
            }
        },
        error: function (res) {
            unBlockUI();
            swal({
                title: "Thông báo",
                text: res.responseText,
                type: 'info',
                confirmButtonClass: 'btn btn-confirm mt-2'
            });
        }
    });
}

function blockUI(msg) {
    $("#maskBoxes").show();
    //$('.maskBoxeContent').css('top', '50%;');
    if (typeof msg != 'undefined')
        $('#msgLoading').text(msg);
}

function unBlockUI() {
    $("#maskBoxes").hide();
}

function _pms_sum(items, prop) {
    return items.reduce(function (a, b) {
        return a + b[prop];
    }, 0);
}
function _defaultErrorHandle(res) {
    unBlockUI();
    alert(res.responseText);
}
function _build_selection_helper($sel, data, callback) {
    $(data).each(function (idx, item) {
        var $op = $("<option />");
        $sel.append($op);
        callback($op, item, idx);
    });
}
function _reset_selection_helper($sel, _default) {
    return $sel.empty().append($("<option value=''></option>").text(_default));
}