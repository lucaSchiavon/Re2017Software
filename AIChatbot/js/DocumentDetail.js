var DocumentDetail = (function () {
   
    var ValidateForm = function (e) {
       
        var OK = true;
        var TxtNomeDocumento = $("#ContentPlaceHolder1_TxtNomeDocumento").val();
        var TxtArgument = $("#ContentPlaceHolder1_TxtArgument").val();
        var TxtDevice = $("#ContentPlaceHolder1_TxtDevice").val();
        var TxtDocNumber = $("#ContentPlaceHolder1_TxtDocNumber").val();
        var CboTypology = $("#ContentPlaceHolder1_CboTypology").val();

        var Fg_TxtNomeDocumento = $("#Fg_TxtNomeDocumento");
        var Fg_TxtArgument = $("#Fg_TxtArgument");
        var Fg_TxtDevice = $("#Fg_TxtDevice");
        var Fg_TxtDocNumber = $("#Fg_TxtDocNumber");
        var Fg_CboTypology = $("#Fg_CboTypology");
        //TxtNomeDocumento
        if (TxtNomeDocumento === "") {
           
            Fg_TxtNomeDocumento.addClass("form-group has-error");
            $("#ContentPlaceHolder1_TxtNomeDocumento").attr('title', "Field can't be empty");  
            OK = false;
        }
        else {
            Fg_TxtNomeDocumento.removeClass("has-error");
        }
       
        //TxtArgument
        if (TxtArgument === "") {
            Fg_TxtArgument.addClass("form-group has-error");
            $("#ContentPlaceHolder1_TxtArgument").attr('title', "Field can't be empty");
            OK = false;
        }
        else {
            Fg_TxtArgument.removeClass("has-error");
        }
        //TxtDevice
        if (TxtDevice === "") {
            Fg_TxtDevice.addClass("form-group has-error");
            $("#ContentPlaceHolder1_TxtDevice").attr('title', "Field can't be empty");
            OK = false;
        }
        else {
            Fg_TxtDevice.removeClass("has-error");
        }
        //TxtDocNumber
        if (TxtDocNumber === "") {
            Fg_TxtDocNumber.addClass("form-group has-error");
            $("#ContentPlaceHolder1_TxtDocNumber").attr('title', "Field can't be empty");
            OK = false;
        }
        else {
            Fg_TxtDocNumber.removeClass("has-error");
        }

        if (CboTypology === "0") {
            Fg_CboTypology.addClass("form-group has-error");
            $(getServerId('CboTypology')).attr('title', "Select a value");
            //CboRolesCtl.attr('title', "Select a value");
            $("#ContentPlaceHolder1_CboTypology").attr('title', "Select a value");
            OK = false;
        }
        else {
            Fg_CboTypology.removeClass("has-error");
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

  
    var CreateGuid =   function () {  
                   function _p8(s) {  
                      var p = (Math.random().toString(16)+"000000000").substr(2,8);  
                      return s ? "-" + p.substr(0,4) + "-" + p.substr(4,4) : p ;  
                   }  
           return _p8() + _p8(true) + _p8(true) + _p8();  
            } 

    function getServerId(id) {
        if (typeof (serverPrefix) !== 'undefined') {
            return document.getElementById(prefix + '_' + id);
        }
        else {
            id;
        }
    }


   


    $(document).on('click', '#ContentPlaceHolder1_LnkBtnSalva', ValidateForm);
    
}());