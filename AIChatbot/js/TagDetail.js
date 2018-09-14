var TagDetail = (function () {
   
    var ValidateForm = function (e) {
        debugger;
        var OK = true;
        var TxtNomeTag = $("#ContentPlaceHolder1_TxtNomeTag").val();
        var TxtMachine = $("#ContentPlaceHolder1_TxtMachine").val();
        var TxtDescription = $("#ContentPlaceHolder1_TxtDescription").val();

        var TxtNode = $("#ContentPlaceHolder1_TxtNode").val();
        var TxtDevice = $("#ContentPlaceHolder1_TxtDevice").val();
        var CboAlarm = $("#ContentPlaceHolder1_CboAlarm").val();
        var TxtValueType = $("#ContentPlaceHolder1_TxtValueType").val();
        var CboTagValue = $("#ContentPlaceHolder1_CboTagValue").val();

        var Fg_TxtNomeTag = $("#Fg_TxtNomeTag");
        var Fg_TxtMachine = $("#Fg_TxtMachine");
        var Fg_TxtDescription = $("#Fg_TxtDescription");

        var Fg_TxtNode = $("#Fg_TxtNode");
        var Fg_TxtDevice = $("#Fg_TxtDevice");
        var Fg_CboAlarm = $("#Fg_CboAlarm");
        var Fg_TxtValueType = $("#Fg_TxtValueType");
        var Fg_CboTagValue = $("#Fg_CboTagValue");

        //NomeTag
        if (TxtNomeTag === "") {
           
            Fg_TxtNomeTag.addClass("form-group has-error");
            $("#ContentPlaceHolder1_TxtNomeTag").attr('title', "Field can't be empty");  
            OK = false;
        }
        else {
            Fg_TxtNomeTag.removeClass("has-error");
        }
       
        //Machine
        if (TxtMachine === "") {
            Fg_TxtMachine.addClass("form-group has-error");
            $("#ContentPlaceHolder1_TxtMachine").attr('title', "Field can't be empty");
            OK = false;
        }
        else {
            Fg_TxtMachine.removeClass("has-error");
        }
        //Description
        if (TxtDescription === "") {
            Fg_TxtDescription.addClass("form-group has-error");
            $("#ContentPlaceHolder1_TxtDescription").attr('title', "Field can't be empty");
            OK = false;
        }
        else {
            Fg_TxtDescription.removeClass("has-error");
        }

        //Node
        if (TxtNode === "") {
            Fg_TxtNode.addClass("form-group has-error");
            $("#ContentPlaceHolder1_TxtNode").attr('title', "Field can't be empty");
            OK = false;
        }
        else {
            Fg_TxtNode.removeClass("has-error");
        }

        //Device
        if (TxtDevice === "") {
            Fg_TxtDevice.addClass("form-group has-error");
            $("#ContentPlaceHolder1_TxtDevice").attr('title', "Field can't be empty");
            OK = false;
        }
        else {
            Fg_TxtDevice.removeClass("has-error");
        }

        //ValueType
        if (TxtValueType === "") {
            Fg_TxtValueType.addClass("form-group has-error");
            $("#ContentPlaceHolder1_TxtValueType").attr('title', "Field can't be empty");
            OK = false;
        }
        else {
            Fg_TxtValueType.removeClass("has-error");
        }

        //CboAlarm
        if (CboAlarm === "-1") {
            Fg_CboAlarm.addClass("form-group has-error");
            $(getServerId('CboAlarm')).attr('title', "Select a value");
            //CboRolesCtl.attr('title', "Select a value");
            $("#ContentPlaceHolder1_CboAlarm").attr('title', "Select a value");
            OK = false;
        }
        else {
            Fg_CboAlarm.removeClass("has-error");
        }

        //CboTagValue
        if (CboTagValue === "0") {
            Fg_CboTagValue.addClass("form-group has-error");
            $(getServerId('CboTagValue')).attr('title', "Select a value");
            //CboRolesCtl.attr('title', "Select a value");
            $("#ContentPlaceHolder1_CboTagValue").attr('title', "Select a value");
            OK = false;
        }
        else {
            Fg_CboTagValue.removeClass("has-error");
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

    $(document).on('click', '#ContentPlaceHolder1_LnkBtnSalva', ValidateForm);
    
}());