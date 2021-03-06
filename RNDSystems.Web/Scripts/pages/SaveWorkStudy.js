﻿$(document).ready(function () {

    if ($('#RecId').val() !== '0') {
        $('#WorkStudyID').prop("readonly", true);
    }
    else
        $('#btnAM').prop("disabled", 'disabled');
    $('#StudyStatus').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
    $('#StudyType').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
    $('#Plant').attr({ 'data-live-search': 'true', 'data-width': '90%' }).selectpicker();
    $('#StartDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#StartDate').datepicker("update", new Date());
    $('#DueDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#CompleteDate').datepicker({ autoclose: true, todayHighlight: true, todayBtn: "linked" });
    $('#btnAdd').on('click', function () {
        location.href = '/WorkStudy/SaveWorkStudy/0'
    });
    $('#btnAM').on('click', function () {
        location.href = '/AssignMaterial/SaveAssignMaterial?id=0&workStudyId=' + $('#WorkStudyID').val()
    });


    //$('#StartDate')
    //    .on('change', function (e) {
    //        console.log('value is : ' + this.value);
    //});

    var form = $('#SaveWorkStudy');
    form.bootstrapValidator({
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            WorkStudyID: {
                validators: {
                    notEmpty: {
                        message: 'WorkStudy ID is required.'
                    }
                }
            },
            StudyType: {
                validators: {
                    callback: {
                        message: 'Study Type is required.',
                        callback: function (value, validator, $field) {
                            /* Get the selected options */
                            var options = validator.getFieldElements('StudyType').val();
                            return (options !== '-1');
                        }
                    }
                }
            },
            Plant: {
                validators: {
                    callback: {
                        message: 'Plant is required.',
                        callback: function (value, validator, $field) {
                            /* Get the selected options */
                            var options = validator.getFieldElements('Plant').val();
                            return (options !== null && options !== '-1');
                        }
                    }
                }
            },
            StudyStatus: {
                validators: {
                    callback: {
                        message: 'Study Status is required.',
                        callback: function (value, validator, $field) {
                            /* Get the selected options */
                            var options = validator.getFieldElements('StudyStatus').val();
                            return (options !== '-1');
                        }
                    }
                }
            },
        }
    });
});


function Submit() {
    var isValid = true;
    if ($('#StartDate').val() === '') {
        isValid = false;
        alert('Start Date is required.');
    }
    //else if ($('#CompleteDate').val() === '') {
    //    isValid = false;
    //    alert('Complete Date is required.');
    //}
    //else if ($('#DueDate').val() === '') {
    //    isValid = false;
    //    alert('Due Date is required.');
    //}
    return isValid;
}