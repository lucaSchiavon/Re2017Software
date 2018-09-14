var Login = (function () {
   
  
    var ValidateForm = function (e) {
       
      
        var email = $("#TxtUsername").val();
        var pwd = $("#TxtPassword").val();

        if (email == "" || pwd == "") {
            if (email == "") {
                var MessErr = $("#Fg_TxtUsername p");
                var Fg_TxtUsername = $("#Fg_TxtUsername");
                Fg_TxtUsername.addClass("form-group has-error");

                MessErr.text("Username can't be empty");
            }
            else
            {
                var Fg_TxtUsername = $("#Fg_TxtUsername");
                Fg_TxtUsername.removeClass("has-error");
                var MessErr = $("#Fg_TxtUsername p");
                MessErr.empty();
            }

            if (pwd == "") {
                var MessErr = $("#Fg_TxtPassword p");
                var Fg_TxtPassword = $("#Fg_TxtPassword");
                Fg_TxtPassword.addClass("form-group has-error");
                MessErr.text("Password can't be empty");
            }
            else {
                var Fg_TxtPassword = $("#Fg_TxtPassword");
                Fg_TxtPassword.removeClass("has-error");
                var MessErr = $("#Fg_TxtPassword p");
                MessErr.empty();
            }
           

            if (e.preventDefault) {
                e.preventDefault();
            }
            else {
                e.returnValue = false;
            }
        }
        else
        {
            //azzera errori
            var Fg_TxtUsername = $("#Fg_TxtUsername");
            Fg_TxtUsername.removeClass("has-error");
            var MessErr = $("#Fg_TxtUsername p");
            MessErr.empty();
            var Fg_TxtPassword = $("#Fg_TxtPassword");
            Fg_TxtPassword.removeClass("has-error");
            var MessErr = $("#Fg_TxtPassword p");
            MessErr.empty();

            e.returnValue = true;
        }
    };



    $(document).on('click', '#BtnLogin', ValidateForm);
    
}());