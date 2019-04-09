dataServie = [];

dataServie.AjaxCall = function (type,url, data, successCallback, errorCallback) {
    try {
        $.ajax({
            url: url,
            type: type,
            dataType: 'json',
            data: data,
            contentType: 'application/json',
            success: successCallback,
            error: errorCallback
        });
    } catch (e) {
        errorCallback
    }
}
