
var URL = 'http://localhost:40001/';
var gloabRowDetails;
var del = true;
function validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1, 3}\.[0-9]{1, 3}\.[0-9]{1, 3}\.[0-9]{1, 3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

$("#txtPhoneNumber").keypress(function (e) {
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        return false;
    }
});


$("#txtUpdatePhoneNumber").keypress(function (e) {
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        return false;
    }
});

function myTrim(x) {
    return x.replace(/^\s+|\s+$/gm, '');
}



function checkVal(obj) {
    $("#" + obj.id).removeClass("error");
}

function EmailValidation(obj) {
    if ($("#" + obj.id).val() == '') {
        $("#" + obj.id).removeClass("error");
    }
    else if (!validateEmail($("#" + obj.id).val())) {
        $("#" + obj.id).addClass("error");
    }
    else {
        $("#" + obj.id).removeClass("error");
    }
}


GetAllContactDetails();

$("body").on("click", "#tblContactDetails .Delete", function () {
    gloabRowDetails = $(this).closest("tr");
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this record.!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then(function (willDelete) {
        if (willDelete) {
            var Deleteparam = {
                DelContactID: gloabRowDetails.find("span").html()
            };
            $.ajax({
                type: "POST",
                url: URL + 'api/Contact/RemoveContact',
                data: Deleteparam,
                content: "application/json; charset=utf-8",
                success: function (response) {
                    if (response.Result) {                       
                        swal("Contact deleted successfully !", {
                            icon: "success",
                        });
                        gloabRowDetails.remove();
                    }
                }
            });
        }
    });
});

$("body").on("click", "#tblContactDetails .edit", function () {
    $('.dvUpdateContainer input[type="text"]').removeClass('error');
    gloabRowDetails = $(this).closest("tr");
    $('#hdnID').val(gloabRowDetails.find("span").html());
    $('#txtUpdateFirstName').val($(this).closest("tr").find("td:eq(1)").text());
    $('#txtUpdateLastName').val($(this).closest("tr").find("td:eq(2)").text());
    $('#txtUpdateEmail').val($(this).closest("tr").find("td:eq(3)").text());
    $('#txtUpdatePhoneNumber').val($(this).closest("tr").find("td:eq(4)").text());
    if ($(this).closest("tr").find("td:eq(5)").text() == 'Active') {
        $("#chkUpdateStatus").prop('checked', true); del = false;
    }
    else {
        $("#chkUpdateStatus").prop('checked', false); del = true;
    }
});

function GetAllContactDetails() {
    $("#tblContactDetails tbody > tr").remove();
    $.ajax({
        type: "GET",
        beforeSend: function () {
            $('.loader').show();
        },
        complete: function () {
            $('.loader').hide();
        },
        url: URL + 'api/Contact/AllContactDetails',
        content: "application/json; charset=utf-8",
        success: function (json) {
            var tr;
            for (var i = 0; i < json.length; i++) {
                tr = $('<tr/>');
                tr.append("<td style='display:none'><span>" + json[i].ID + "</span></td>");
                tr.append("<td>" + json[i].FirstName + "</td>");
                tr.append("<td>" + json[i].LastName + "</td>");
                tr.append("<td>" + json[i].Email + "</td>");
                tr.append("<td>" + json[i].PhoneNumber + "</td>");
                if (json[i].Status) {
                    tr.append("<td>Active</td>");
                }
                else {
                    tr.append("<td>Inactive</td>");
                }
                tr.append("<td><i data-toggle='modal' data-target='.bd-example-modal-lg' class='fa fa-pencil-square-o edit' aria-hidden='true' style='font-size: 36px; margin-right: 20px;cursor:pointer'></i><i class='fa fa-trash-o Delete' style='font-size: 36px;cursor:pointer'></i></td>");
                $('table').append(tr);
            }
        }
    });
}
function Reset() {
    $('.dvContainer input[type="text"]').val('');
    $('#chkStatus').removeAttr('checked');
    $('.dvContainer input[type="text"]').removeClass('error');
}
function Save_Update_ContactInfo(obj) {
    var chkvalue = false;
    var dataURL = '';
    var tblContactInfo = {};
    var allvalidate = 1;
    if (obj == 'Save') {
        dataURL = URL + 'api/Contact/ContactSubmission';
        if ($("#chkStatus").prop('checked') == true) {
            chkvalue = true;
        }

        $(".dvContainer input[type=text]").each(function () {
            if ($(this).val() == '') {
                $(this).addClass('error');
                allvalidate = 0;
            }
            else if ($(this)[0].id == 'txtEmail') {
                if (!validateEmail($(this).val())) {
                    allvalidate = 0;
                }
                else {
                    allvalidate = 1;
                    $(this).removeClass('error');
                }
            }
        });

        tblContactInfo = {
            FirstName: $('#txtFirstName').val(),
            LastName: $('#txtLastName').val(),
            Email: $('#txtEmail').val(),
            PhoneNumber: $('#txtPhoneNumber').val(),
            Status: chkvalue
        };

    }
    else {
        dataURL = URL + 'api/Contact/UpdateSubmission';
        if ($("#chkUpdateStatus").prop('checked') == true) {
            chkvalue = true;
        }

        $(".dvUpdateContainer input[type=text]").each(function () {
            if ($(this).val() == '') {
                $(this).addClass('error');
                allvalidate = 0;
            }
            else if ($(this)[0].id == 'txtUpdateEmail') {
                if (!validateEmail($(this).val())) {
                    allvalidate = 0;
                }
                else {
                    allvalidate = 1;
                    $(this).removeClass('error');
                }
            }
        });
        tblContactInfo = {
            FirstName: $('#txtUpdateFirstName').val(),
            LastName: $('#txtUpdateLastName').val(),
            Email: $('#txtUpdateEmail').val(),
            PhoneNumber: $('#txtUpdatePhoneNumber').val(),
            Status: chkvalue,
            ID: $('#hdnID').val()
        };
    }
    if (allvalidate == 1) {
        $.ajax({
            type: "POST",
            url: dataURL,
            data: tblContactInfo,
            content: "application/json; charset=utf-8",
            success: function (response) {
                if (response.Result) {
                    if (obj == 'Save') {
                        swal("Contact saved successfully!", "", "success");
                        $('.dvContainer input[type="text"]').val('');
                        $('.dvContainer input[type="text"]').removeClass('error');
                        $('#chkStatus').removeAttr('checked');
                        GetAllContactDetails();
                    }
                    else {
                        swal("Contact updated successfully!", "", "success");
                        gloabRowDetails.find("td:eq(1)").html($('#txtUpdateFirstName').val());
                        gloabRowDetails.find("td:eq(2)").html($('#txtUpdateLastName').val());
                        gloabRowDetails.find("td:eq(3)").html($('#txtUpdateEmail').val());
                        gloabRowDetails.find("td:eq(4)").html($('#txtUpdatePhoneNumber').val());
                        if (chkvalue) {
                            gloabRowDetails.find("td:eq(5)").html('Active');
                        }
                        else {
                            gloabRowDetails.find("td:eq(5)").html('Inactive');
                        }
                        $("#editModle").modal('hide');
                        $('.dvUpdateContainer input[type="text"]').val('');
                        $('#chkUpdateStatus').removeAttr('checked');
                    }
                }
                else {
                    alert('Something went wrong');
                }
            }
        });
    }

}
