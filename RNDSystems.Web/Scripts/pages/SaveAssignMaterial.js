$(document).ready(function () {
    //var UAC_Part;
    //var SelectedAlloy;
    //var SelectedTemper;
    var SelectedDataBase;
    //= "US";

    $("#MillLotNo").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });

    $("#UACPart").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
    /*
    $('#Alloy').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
    $('#Temper').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
    */
    $('#DataBase').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
    
    if ($('#RecID').val() !== '0') {
        $('#WorkStudyID').prop("readonly", true);        
    }    

    if ($('#MillLotNo').val() === '0') {
        $('#MillLotNo').val('');
    }

    if ($('#MillLotNo').val()) {
      
        $('#MillLotNo').prop("readonly", true);        
        $('#btnSelected').prop('disabled', false);
        MillLot_No = $('#MillLotNo').val();
        LoadGrid("UACRepeater");
    }
    else {
        $('#MillLotNo').prop("readonly", false);
        $('#btnSelected').prop('disabled', true);
    }    

    $("#MillLotNo").change(function () {
          
        var RecID = $("#RecID").val();               
        if ((RecID == "0" || RecID == undefined) && $('#MillLotNo').val()) {
            var LotNo = $('#MillLotNo').val();
          
            var options = {                
                MillLotNo: $('#MillLotNo').val(),
                recID: 0,
                DataBaseName: SelectedDataBase
            };
            $.ajax({
                url: Api + "api/AssignMaterial/",
                headers: {
                    Token: GetToken()
                },
                type: 'Get',
                data: options,
                async: false,
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data && data.MillLotNo > 0) {                        
                        $("#CustPart").val(data.CustPart);
                        $("#UACPart").val(data.UACPart);
                        $("#Alloy").val(data.Alloy);
                        $("#Temper").val(data.Temper);
                        $("#SoNum").val(data.SoNum);                       
                        /*
                        var output = data.ddlAlloy;
                        var optionhtml1 = '<option value="' +
                            0 + '">' + "--Select State--" + '</option>';
                        //$("#Alloy").append(optionhtml1);
                        var dataValue;
                        var dataText;

                        //loadSelectItems($("#Alloy"), output);

                        var optionhtml;
                        $.each(output, function (i) {
                          
                            dataValue = output[i].Value;
                            dataText = output[i].Text;

                            optionhtml += '<option value="' +
                                output[i].Value + '">' + output[i].Text + '</option>';
                        });
                        
                        $("#Alloy").empty();
                        $("#Alloy").append(optionhtml);
                        $("#Alloy").selectpicker('refresh');
                        */
                    
                        JqueryFunction.ReadOnly();
                  
                        $('#btnSelected').prop('disabled', false);                        

                        if ($('#MillLotNo').val()) {
                            MillLot_No = $('#MillLotNo').val();                            
                        }

                        if ($('#UACPart').val()) {
                            UACPart = $('#UACPart').val();
                           // UAC_Part = $('#UACPart').val();                            
                        }

                        if (typeof MillLot_No !== 'undefined' && MillLot_No != null && typeof UACPart !== 'undefined' && UACPart != null)
                        {
                            LoadGrid("UACRepeater");                            
                        }
                    }
                    else {
                        JqueryFunction.ReadOnly();
                        $('#btnSelected').prop('disabled', true);
                    }
                },
                error: function (x, y, z) {
                }
            });

        }
    });

    /*
    $("#Alloy").on('change', function () {     
        SelectedAlloy = $(this).find("option:selected").val();
      
        if (SelectedAlloy != null && SelectedAlloy != undefined && SelectedAlloy != "-1") {
            var InPutParameter = {
                Alloy: SelectedAlloy,
                MillLotID: 0
            };           

            $.ajax({
                url: Api + "api/AssignMaterial/",
                headers: {
                    Token: GetToken()
                },
                type: 'Get',
                data: InPutParameter,
                async: false,
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data) {
                        
                        var output = data.ddlTemper;
                        var optionhtml1 = '<option value="' +
                            0 + '">' + "--Select State--" + '</option>';
                        //$("#Alloy").append(optionhtml1);
                        var dataValue;
                        var dataText;

                        //loadSelectItems($("#Alloy"), output);

                        var optionhtml;
                        $.each(output, function (i) {
                           
                            dataValue = output[i].Value;
                            dataText = output[i].Text;

                            optionhtml += '<option value="' +
                                output[i].Value + '">' + output[i].Text + '</option>';
                        });
                        $("#Temper").empty();
                        $("#Temper").append(optionhtml);
                        $("#Temper").selectpicker('refresh');
                     
                    }
                },
                error: function (x, y, z) {
                }
            });
        }
    });
   */
 /* 
    $("#Temper").on('change', function () {       
        SelectedTemper = $(this).find("option:selected").val();      
    });
 */
    $("#DataBase").on('change', function () {
        //debugger;         
        SelectedDataBase = $(this).find("option:selected").val();
        if (SelectedDataBase == "-1")
            SelectedDataBase = "US";
    });
    

    function loadSelectItems(select, items) {    
        var options = '';
        var dataValue;
        var dataKey;

        $.each(items, function (key, value) {     
            dataKey = key;
            dataValue = value;
            options += '<option value=' + value + '>' + value + '</option>';
        });

        select.empty();
        select.append(options);
        select.selectpicker('refresh');
    }

    if ($('#UACPart').val() === '0') {
        $('#UACPart').val('');
    }

    $('#btnAdd').on('click', function () {
        console.log('cliked');
        location.href = '/AssignMaterial/SaveAssignMaterial/0';
    });

    $('#btnSelected').on('click', function () {   
        var ids = '';
        $("a[name='pp_as']").each(function () {
            ids += $(this).attr('id').replace('pp_', '') + ';';
        });
        if ($('#MillLotNo').val() !== '' && $('#MillLotNo').val() !== '0') {
            //var obj = {
            //    Comment: ids, MillLotNo: $('#MillLotNo').val(), WorkStudyID: $('#WorkStudyID').val()
            //};

            var obj = {
                records: ids, MillLotNo: $('#MillLotNo').val(), WorkStudyID: $('#WorkStudyID').val(),
                WorkStudyID: $('#WorkStudyID').val(), SoNum: $("#SoNum").val(),
                //UACPartNo: $('#UACPart').val(),
                // UACPartNo: UAC_Part,
                // UacPartNo: UAC_Part,
                UacPartNo: UACPart,
                Comment: $('#Comment').val()
                , Alloy: $("#Alloy").val()
                , Temper: $("#Temper").val()
                , PieceNo: $("#PieceNo").val()
                , Hole: $("#Hole").val()
                //, DataBaseName: SelectedDataBase
                //, Alloy: SelectedAlloySend, Temper: SelectedTemperSend
            };
            //var SelectedAlloySend = null;
            //$("#Alloy").on('change', function () {
                
            //    SelectedAlloySend = $(this).find("option:selected").val();
            //});
          
            //var SelectedTemperSend = null;
            //$("#Temper").on('change', function () {
              
            //    SelectedTemperSend = $(this).find("option:selected").val();
            //});
                //var obj = {
                //    records: ids, MillLotNo: $('#MillLotNo').val(), WorkStudyID: $('#WorkStudyID').val(),
                //    WorkStudyID: $('#WorkStudyID').val(), SoNum: $("#SoNum").val(), UACPartNo: $('#UACPart').val(),
                //    Comment: $('#Comment').val()
                //    //, Alloy: SelectedAlloySend, Temper: SelectedTemperSend
                //};
           
            var json = JSON.stringify(obj);
            $.ajax({
                url: Api + "api/UACListing",
                headers: {
                    Token: GetToken()
                },
                type: 'Post',
                data: json,
                //data: JSON.stringify(ApiViewModel),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data) {
                        if (!data.Success)
                            bootbox.alert(data.Message);
                        else {

                        }
                    }
                },
                error: function (x, y, z) {
                }
            });
       
        }
        else
            alert('Enter MillLotNo');
    });

    JqueryFunction = {
        ReadOnly: function () {
            
            var inp = $("#MillLotNo").val();

            if (jQuery.trim(inp).length > 0) {
                $('#MillLotNo').prop("readonly", true);
            }
            else {
                $('#MillLotNo').prop("readonly", false);
            }

            inp = $("#CustPart").val();

            if (jQuery.trim(inp).length > 0) {
                $('#CustPart').prop("readonly", true);
            }
            else {
                $('#CustPart').prop("readonly", false);
            }

            inp = $("#UACPart").val();

            if (jQuery.trim(inp).length > 0) {
                $('#UACPart').prop("readonly", true);
            }
            else {
                $('#UACPart').prop("readonly", false);
            }
            
            inp = $("#Alloy").val();

            if (jQuery.trim(inp).length > 0) {
                $('#Alloy').prop("readonly", true);
            }
            else {
                $('#Alloy').prop("readonly", false);
            }
            
            inp = $("#Temper").val();

            if (jQuery.trim(inp).length > 0) {
                $('#Temper').prop("readonly", true);
            }
            else {
                $('#Temper').prop("readonly", false);
            }
            
            inp = $("#SoNum").val();

            if (jQuery.trim(inp).length > 0) {
                $('#SoNum').prop("readonly", true);
            }
            else {
                $('#SoNum').prop("readonly", false);
            }           
        }
    }
        
    //$('#btnUACPart').on('click', function () {
    //    //$('#ppUACListing').show();
    //    //var div = $('#ppUACListing').html();
    //    //dialog = bootbox.dialog({
    //    //    message: div,
    //    //    size: 'large',
    //    //    buttons: {
    //    //        cancel: {
    //    //            label: '<i class="fa fa-times"></i> Cancel',
    //    //            className: 'btn-danger'
    //    //        },
    //    //        confirm: {
    //    //            label: '<i class="fa fa-check"></i> Save',
    //    //            className: 'btn-success',
    //    //            callback: function (result) {
    //    //                console.log(result);
    //    //            }
    //    //        },
    //    //    },
    //    //    onEscape: function () {
    //    //        this.modal('hide');
    //    //    }
    //    //})
    //    //var obj = {
    //    //    id: $('#RecId').val(), MillLotNo: $('#MillLotNo').val()
    //    //};
    //    //$.ajax({
    //    //    type: 'post',
    //    //    url: GetRootDirectory() + '/AssignMaterial/UACListing',
    //    //    data: obj
    //    //}).done(function (data) {
    //    //    $('#ppUACListing').html(data);
    //    //    var div = $('#ppUACListing').html();
    //    //    dialog = bootbox.dialog({
    //    //        message: div,
    //    //        size: 'large',
    //    //        buttons: {
    //    //            cancel: {
    //    //                label: '<i class="fa fa-times"></i> Cancel',
    //    //                className: 'btn-danger'
    //    //            },
    //    //            confirm: {
    //    //                label: '<i class="fa fa-check"></i> Save',
    //    //                className: 'btn-success',
    //    //                callback: function (result) {
    //    //                    console.log(result);
    //    //                }
    //    //            },
    //    //        },
    //    //        onEscape: function () {
    //    //            this.modal('hide');
    //    //        }
    //    //    })
    //    //});
    //});
        
     //$('#StartDate')
    //    .on('change', function (e) {
    //        console.log('value is : ' + this.value);
    //});

    var form = $('#SaveAssignMaterial');
    form.bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            MillLotNo: {
                validators: {
                    notEmpty: {
                        message: 'Mill Lot No. is required.'
                    }
                }
            },
            UACPart: {
                validators: {
                    notEmpty: {
                        message: 'UAC Part No is required.'
                    }
                }
            },
            Alloy: {
                validators: {
                    notEmpty: {
                        message: 'Alloy is required.'
                    }
                }
            },
            Temper: {
                validators: {
                    notEmpty: {
                        message: 'Temper is required.'
                    }
                }
            },
        }
    });
});


//function Submit() {
//    var isValid = true;
//    //if ($('#MillLotNo').val() === '') {
//    //    isValid = false;
//    //    alert('Mill LotNo is required.');
//    //}
//    //else if ($('#CompleteDate').val() === '') {
//    //    isValid = false;
//    //    alert('Complete Date is required.');
//    //}
//    //else if ($('#DueDate').val() === '') {
//    //    isValid = false;
//    //    alert('Due Date is required.');-             
//    //}
//    return isValid;
//}