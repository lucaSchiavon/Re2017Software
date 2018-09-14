var UserDetail = (function () {
   
    var ValidateForm = function (e) {
        
        var OK = true;
        var TxtNomeUtente = $("#ContentPlaceHolder1_TxtNomeUtente").val();
        var CboRoles = $("#ContentPlaceHolder1_CboRoles").val();
        var TxtUsername = $("#ContentPlaceHolder1_TxtUsername").val();
        var TxtPassword = $("#ContentPlaceHolder1_TxtPassword").val();

        var Fg_TxtNomeUtente = $("#Fg_TxtNomeUtente");
        var Fg_CboRoles = $("#Fg_CboRoles");
        var Fg_TxtUsername = $("#Fg_TxtUsername");
        var Fg_TxtPassword = $("#Fg_TxtPassword");
        //Nome utente
        if (TxtNomeUtente === "") {
           
            Fg_TxtNomeUtente.addClass("form-group has-error");
            $("#ContentPlaceHolder1_TxtNomeUtente").attr('title', "Field can't be empty");  
            OK = false;
        }
        else {
            Fg_TxtNomeUtente.removeClass("has-error");
        }
        //Cbo roles
        if (CboRoles === "0") {  
            Fg_CboRoles.addClass("form-group has-error");
            $(getServerId('CboRoles')).attr('title', "Select a value");
            //CboRolesCtl.attr('title', "Select a value");
            $("#ContentPlaceHolder1_CboRoles").attr('title', "Select a value");
            OK = false;
        }
        else {
            Fg_CboRoles.removeClass("has-error");
        }
       
        //UserName
        if (TxtUsername === "") {
            Fg_TxtUsername.addClass("form-group has-error");
            $("#ContentPlaceHolder1_TxtUsername").attr('title', "Field can't be empty");
            OK = false;
        }
        else {
            Fg_TxtUsername.removeClass("has-error");
        }
        //Password
        if (TxtPassword === "") {
            Fg_TxtPassword.addClass("form-group has-error");
            $("#ContentPlaceHolder1_TxtPassword").attr('title', "Field can't be empty");
            OK = false;
        }
        else {
            Fg_TxtPassword.removeClass("has-error");
        }
        //Password check
        if (!checkStrength(TxtPassword)) {
            Fg_TxtPassword.addClass("form-group has-error");
            $("#ContentPlaceHolder1_TxtPassword").attr('title', "Password must contain at least 6 caracters of numbers and letters");
            OK = false;
        }
        else {
            Fg_TxtPassword.removeClass("has-error");
        }


        if (!OK) {
            if (e.preventDefault) {
                e.preventDefault();
            }
            else {
                e.returnValue = false;
            }
        }
        else
        {
            e.returnValue = true;
        }

    };

    function getServerId(id) {
        if (typeof (serverPrefix) !== 'undefined') {
            return document.getElementById(prefix + '_' + id);
        }
        else {
            id;
        }
    }

    function checkStrength(password) {
        var OK = true;
        if (password.length < 6) {
          
            OK= false;
        }
        // If it has numbers and characters, increase strength value.
        if (!password.match(/([a-zA-Z])/) && password.match(/([0-9])/))
        {
            OK = false;
        }
        return OK;
       
    }


    $(document).on('click', '#ContentPlaceHolder1_LnkBtnSalva', ValidateForm);
    
}());