$(document).ready(function () {
    //debugger;
    if ($('#RecId').val() !== '0') {
        $('#WorkStudyID').prop("readonly", true);        
    }
    else
    {
        $('#btnPM').prop("disabled", 'disabled');
    }

    //$('#StudyStatus').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
    //$('#StudyType').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
    //$('#Plant').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
    //$('#StartDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    //$('#StartDate').datepicker("update", new Date());
    //$('#DueDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    //$('#CompleteDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });

    //$('#ddlHours').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
    $('#SHTStartHrs').attr('data-live-search', 'true');
    $('#SHTStartHrs').selectpicker();

    $('#SHTStartMns').attr('data-live-search', 'true');
    $('#SHTStartMns').selectpicker();

    $('#ArtStartHrs').attr('data-live-search', 'true');
    $('#ArtStartHrs').selectpicker();

    $('#ArtStartMns').attr('data-live-search', 'true');
    $('#ArtStartMns').selectpicker();

    $('#ddlMillLotNo').attr('data-live-search', 'true');
    $('#ddlMillLotNo').selectpicker();

    $('#ddlHole').attr('data-live-search', 'true');
    $('#ddlHole').selectpicker();

    $('#ddlPieceNo').attr('data-live-search', 'true');
    $('#ddlPieceNo').selectpicker();

    if ($('#ddlMillLotNo').val() === '0') {
        $('#ddlMillLotNo').val('');
    }

    //if ($('#ProcessNo').val() === '0') {
    //    $('#ProcessNo').val('');
    //}

    //if ($('#AgeLotNo').val() === '0') {
    //    $('#AgeLotNo').val('');
    //}

    //if ($('#HTLogNo').val() === '0') {
    //    $('#HTLogNo').val('');
    //}

    $('#btnAdd').on('click', function () {
        location.href = '/ProcessingMaterial/SaveProcessingMaterial/0'
    });
    $('#btnPM').on('click', function () {
        location.href = '/ProcessingMaterial/SaveProcessingMaterial?id=0&workStudyId=' + $('#WorkStudyID').val()
    });


    //$('#StartDate')
    //    .on('change', function (e) {
    //        console.log('value is : ' + this.value);
    //});

    $('#SHTDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#ArtAgeDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });

    var form = $('#SaveProcessingMaterial');
    form.bootstrapValidator({        
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },        
        fields: {
            SHTTemp: {
                validators: {
                    notEmpty: {
                        message: 'SHTTemp is required.'
                    }
                }
            },
            SHTStartHrs: {
                validators: {
                    callback: {
                        //message: 'Study Type is required.',
                        callback: function (value, validator, $field) {
                            /* Get the selected options */
                            var options = validator.getFieldElements('SHTStartHrs').val();
                            return (options !== '-1');
                        }
                    }
                }
            },
            SHTStartMns: {
                validators: {
                    callback: {
                        //message: 'Study Type is required.',
                        callback: function (value, validator, $field) {
                            /* Get the selected options */
                            var options = validator.getFieldElements('SHTStartMns').val();
                            return (options !== '-1');
                        }
                    }
                }
            },
            ArtStartHrs: {
                validators: {
                    callback: {
                        //message: 'Study Type is required.',
                        callback: function (value, validator, $field) {
                            /* Get the selected options */
                            var options = validator.getFieldElements('ArtStartHrs').val();
                            return (options !== '-1');
                        }
                    }
                }
            },
            ArtStartMns: {
                validators: {
                    callback: {
                        //message: 'Study Type is required.',
                        callback: function (value, validator, $field) {
                            /* Get the selected options */
                            var options = validator.getFieldElements('ArtStartMns').val();
                            return (options !== '-1');
                        }
                    }
                }
            },
        }
    });
});