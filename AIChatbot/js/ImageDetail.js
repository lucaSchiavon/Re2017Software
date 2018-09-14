var ImageDetail = (function () {
   
    var ValidateForm = function (e) {
        debugger;
        var OK = true;
        var TxtNomeImmagine = $("#ContentPlaceHolder1_TxtNomeImmagine").val();
        var TxtArgument = $("#ContentPlaceHolder1_TxtArgument").val();
        var TxtDevice = $("#ContentPlaceHolder1_TxtDevice").val();
        var TxtTags = $("#ContentPlaceHolder1_TxtTags").val();
        var TxtDescription = $("#ContentPlaceHolder1_TxtDescription").val();
      

        var Fg_TxtNomeImmagine = $("#Fg_TxtNomeImmagine");
        var Fg_TxtArgument = $("#Fg_TxtArgument");
        var Fg_TxtDevice = $("#Fg_TxtDevice");
        var Fg_TxtTags = $("#Fg_TxtTags");
        var Fg_TxtDescription = $("#Fg_TxtDescription");
        
        //TxtNomeDocumento
        if (TxtNomeImmagine === "") {
           
            Fg_TxtNomeImmagine.addClass("form-group has-error");
            $("#ContentPlaceHolder1_TxtNomeImmagine").attr('title', "Field can't be empty");  
            OK = false;
        }
        else {
            Fg_TxtNomeImmagine.removeClass("has-error");
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
        if (TxtTags === "") {
            Fg_TxtTags.addClass("form-group has-error");
            $("#ContentPlaceHolder1_TxtTags").attr('title', "Field can't be empty");
            OK = false;
        }
        else {
            Fg_TxtTags.removeClass("has-error");
        }

        //TxtDescription
        if (TxtDescription === "") {
            Fg_TxtDescription.addClass("form-group has-error");
            $("#ContentPlaceHolder1_TxtDescription").attr('title', "Field can't be empty");
            OK = false;
        }
        else {
            Fg_TxtDescription.removeClass("has-error");
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

  
    //var CreateGuid =   function () {  
    //               function _p8(s) {  
    //                  var p = (Math.random().toString(16)+"000000000").substr(2,8);  
    //                  return s ? "-" + p.substr(0,4) + "-" + p.substr(4,4) : p ;  
    //               }  
    //       return _p8() + _p8(true) + _p8(true) + _p8();  
    //        } 

    //function getServerId(id) {
    //    if (typeof (serverPrefix) !== 'undefined') {
    //        return document.getElementById(prefix + '_' + id);
    //    }
    //    else {
    //        id;
    //    }
    //}

    $(document).on('click', '#ContentPlaceHolder1_LnkBtnSalva', ValidateForm);
    
}());