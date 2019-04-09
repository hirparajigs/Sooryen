user = [];

user.Login = function () {
    userData = {
        UserName: $("userName").val(),
        Password: $("Password").val()
    };
    dataServie.AjaxCall('post', '/User/UserLogin', { userData }, function (data) {
        if (data.UserId == "") {
            alert("Invalid Password/User Id");}
        else { successCallback() }
      
    }, function (err) {
        alert(err);
    });
    
}

function successCallback() {
    window.location.href = '/Note/Notes/';
}


