let SignUp = () => {

    let User = {
        UserName: document.getElementById("UserName").value,
        Email: document.getElementById("Email").value,
        Mobile : document.getElementById("Mobile").value,
        password: document.getElementById("password").value,
        ConfirmPassWord : document.getElementById("ConfirmPassWord").value
    }


    $.ajax({
        type: 'POST',
        url: '/Home/SignUpPost',
        data: { NewUser: User },
        success: function (data) {
            if (data == "Done") {
                alert("Done");
            } else {
                alert("Failed")
            }
        }
    });
}
