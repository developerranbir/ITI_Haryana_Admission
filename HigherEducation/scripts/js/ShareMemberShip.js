$("#ShareSubmitId").click(function () {
    debugger;
    var Imgpath = $('#Img_files_10').text();
    $("#ImgPath").val(Imgpath);
    var i;
    var isValid = "Yes";
    var files = "";
    for (i = 0; i <= 4; i++) {
        files = "files_" + i;
        if (document.getElementById(files).files.length == 0) {
            isValid = "No";
        }
    }
    if (isValid == "No") {
        alert("Fill Mandatory Field First");
        event.preventDefault();
    }
});
$("#udClick,#FirstClick").click(function () {
    var value = "";
    if ($('#CommunityofSociety').val() == 1) {
        value = "Employee";
    }
    else if ($('#CommunityofSociety').val() == 2) {
        value = "Defence/Ex Serviceman";
    }
    else if ($('#CommunityofSociety').val() == 3) {
        value = "General";
    }
    else if ($('#CommunityofSociety').val() == 4) {
        value = "SC";
    }
    else if ($('#CommunityofSociety').val() == 5) {
        value = "PH";
    }
    else if ($('#CommunityofSociety').val() == 6) {
        value = "EWS";
    }
    else if ($('#CommunityofSociety').val() == 7) {
        value = "Others";
    }
    $('#labelValue').text(value);
});
$("#IsCheckedId").click(function () {
    if ($("#IsCheckedId").val() == "on") {
        $('#P_Address1').val($('#Present_Address1').val());
        $('#P_DistrictOfMember').val($('#Present_DistrictOfMember').val());
        $('#P_PostOfficeOfSocietyMember').val($('#Present_PostOfficeOfSocietyMember').val());
        $('#P_PostalCodeOfSocietyMember').val($('#Present_PostalCodeOfSocietyMember').val());
        $('#P_Address2').val($('#Present_Address2').val());
        $('#IsCheckedId').val("true")
    }
    else if ($("#IsCheckedId").val() == "true") {
        $('#P_Address1').val("");
        $('#P_DistrictOfMember').val("");
        $('#P_PostOfficeOfSocietyMember').val("");
        $('#P_PostalCodeOfSocietyMember').val("");
        $('#P_Address2').val("");
        $('#IsCheckedId').val("on")
    }
});
$("#showList").click(function () {
    loadShareTtransferMemberShip()
});

function loadShareTtransferMemberShip() {
    var SocietyTransID = "";
    $.ajax({
        url: "/ShareTransfer/ShareMemberShipList/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: { SocietyTransID },
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.MemberName + '</td>';               
                html += '<td>' + item.TransferorName + '</td>';
                html += '<td>' + item.createddate + '</td>';
                html += '<td><a id="ShareMembershipDetail" href="#" class="btn btn-block ink-reaction btn-flat btn-info" onclick="return GetbyShareTransferID(' + item.IdentityNo + ')">View</a></td > ';
                html += '<td><a id="ShareMembershipDetail" href="#" class="btn btn-block ink-reaction btn-flat btn-info" onclick="return DeleleMembership(' + item.IdentityNo + ')">Delete</a></td > ';
                html += '<td><a id="ShareMembeccrshipDetail" href="#" class="btn btn-block ink-reaction btn-flat btn-info" onclick="return ForwardMembership(' + item.IdentityNo + ')">Forward</a></td > ';
                html += '</tr>';
            });
            $('.tblShareMembership').html(html);
        },
        error: function (errormessage) {
            //alert(errormessage.responseText);
        }
    });
}
function GetbyShareTransferID(IdentityNo) {
    sessionStorage.setItem("clickId", IdentityNo);
  //  $('#mymodalOption').modal('show');
    BindSocietyId(IdentityNo);
    debugger;
    $('#files_1').removeAttr("required");
    $('#files_2').removeAttr("required");
    $('#files_3').removeAttr("required");
    $('#files_4').removeAttr("required");
    $('#files_5').removeAttr("required");
    $('#files_6').removeAttr("required");
    $('#files_7').removeAttr("required");
    $('#files_8').removeAttr("required");
    $('#files_9').removeAttr("required");
    $('#files_10').removeAttr("required");
    $('#files_11').removeAttr("required");
    $('#files_12').removeAttr("required");
    $('#files_13').removeAttr("required");
    $('#files_14').removeAttr("required");
    $('#files_15').removeAttr("required");
    $('#files_16').removeAttr("required");
    $('#files_17').removeAttr("required");
    $('#files_18').removeAttr("required");
    $('#mytabs a[href="#firstShare"]').tab('show');
}
function DeleleMembership(IdentityNo) {
    var status = $('#offdelete').val();
    var isValid = true;
    if (status === "1") {
        isValid = false;
        alert("You can't delete this entry .");
        return isValid;
    }
    var ans = confirm("Are You Sure To Delete The Share Transfer Membership?");
    if (ans) {
        var objMFD = {
            IdentityNo: IdentityNo
        };
        $.ajax({
            url: "/ShareTransfer/DeleteMembership",
            data: JSON.stringify(objMFD),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                alert("Deleted Successfully")
                loadShareTtransferMemberShip();
            },
            error: function (errormessage) {
                //alert(errormessage.responseText);
            }
        });
    }
}
function ForwardMembership(IdentityNo) { 
    var ans = confirm("Are You Sure You Want To Forward this to ARCS Officer?");
    if (ans) {
        var objMFD = {
            IdentityNo: IdentityNo
        };
        $.ajax({
            url: "/ShareTransfer/ForwardMembership",
            data: JSON.stringify(objMFD),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {        
                if (result.IsCount == 18) {
                    alert("Share Transfer has been Successfully Forwarded To ARCS Officer.");
                    loadShareTtransferMemberShip();
                }
                else {
                    alert("Please attest Complete Documents First in Upload Document Section");
                }               
                
            },
            error: function (errormessage) {
                //alert(errormessage.responseText);
            }
        });
    }            
}
function GetSoceityData(societyTransId) {
    $.ajax({
        url: "/ShareTransfer/GetSoceityData/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: { societyTransId },
        success: function (result) {
            var getbySociety = result[0];
            $('#showSocietyName').val(getbySociety.SocietyName);
            $('#showSocietyAddress1').val(getbySociety.Address1);
            $('#showSocietyDistrict').val(getbySociety.Address2);
            $('#showSocietyPostOffice').val(getbySociety.PostOffice);
            $('#showSocietyPostalCode').val(getbySociety.PostalCode1);
            var value = result[0].AreaOfOperation;
            $('#showAOP').val(value);
            $('#showSocietyCategory').val(getbySociety.SocietyClassName);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function GetTransfereeData(MemberSNo) {
    $.ajax({
        url: "/ShareTransfer/GetTransfereeData/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: { MemberSNo },
        success: function (result) {
            var getbySocietyMemberID = result[0];
            $('#showName1').val(getbySocietyMemberID.MemberName);
            $('#showFatherName1').val(getbySocietyMemberID.FatherName);
            $('#showAge').val(getbySocietyMemberID.Age);
            $('#showAddress1').val(getbySocietyMemberID.Address1);
            $('#showPostOffice').val(getbySocietyMemberID.PostOffice);
            $('#PostalCodeOfSocietyMember').val(getbySocietyMemberID.Pin);
            $('#showAddress2').val(getbySocietyMemberID.Address2);
            $('#showimg').attr('src', getbySocietyMemberID.Fullpath);
            $('#showDistrict').val(getbySocietyMemberID.DistCode);            
            $('#showimg').val(getbySocietyMemberID.imgg);
        },
        error: function (errormessage) {
            //alert(errormessage.responseText);
        }
    });
    return false;
}
function BindSocietyId(IdentityNo) {
    
    $.ajax({
        url: "/ShareTransfer/GetbyShareTransferID/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: { IdentityNo },
        success: function (result) {
            debugger;
            var GetTransferMemberDetail = result;
            $('#MemberName').val(GetTransferMemberDetail.MemberName);
            $('#FatherName').val(GetTransferMemberDetail.FatherName);
            $('#ManagingMemberRelationship').val(GetTransferMemberDetail.ManagingMemberRelationship);
            $('#GenderOfSocietyMember').val(GetTransferMemberDetail.Gender);
            $('#dateofbirth').val(GetTransferMemberDetail.Dob);
            $('#CommunityofSociety').val(GetTransferMemberDetail.communityTypeId);
            $('#categoryIdTrans').val(GetTransferMemberDetail.communityTypeId);
            $('#Transferee_Age').val(GetTransferMemberDetail.Age);
            $('#MobileNumberOfSocietyMember').val(GetTransferMemberDetail.Mobile);
            $('#EmailId').val(GetTransferMemberDetail.EmailId);
            $('#AadharNo1').val(GetTransferMemberDetail.AadharNo);
            $('#OccupationOfMember').val(GetTransferMemberDetail.OccupationName);
            $('#NoofSharesSubscribed').val(GetTransferMemberDetail.NoOfShares);
            $('#NameofNominee').val(GetTransferMemberDetail.NomineeName);
            $('#NomineeAge').val(GetTransferMemberDetail.NomineeAge);
            $('#RelationshipCodeOfSocietyMember').val(GetTransferMemberDetail.RelationshipCode);
            $('#Present_Address1').val(GetTransferMemberDetail.Address1);
            $('#Present_DistrictOfMember').val(GetTransferMemberDetail.DistCode);
            $('#Present_PostOfficeOfSocietyMember').val(GetTransferMemberDetail.PostOffice);
            if (GetTransferMemberDetail.Pin == 0) {
                $('#Present_PostalCodeOfSocietyMember').val("");
            }
            else {
                $('#Present_PostalCodeOfSocietyMember').val(GetTransferMemberDetail.Pin);
            }
            $('#Present_Address2').val(GetTransferMemberDetail.Address2);
            $('#P_Address1').val(GetTransferMemberDetail.Permanent_Address1);
            $('#P_DistrictOfMember').val(GetTransferMemberDetail.Permanent_DistCode);
            $('#P_PostOfficeOfSocietyMember').val(GetTransferMemberDetail.Permanent_PostOffice);
            if (GetTransferMemberDetail.Permanent_Pin == 0) {
                $('#P_PostalCodeOfSocietyMember').val("");
            }
            else {
                $('#P_PostalCodeOfSocietyMember').val(GetTransferMemberDetail.Permanent_Pin);
            }
            $('#P_Address2').val(GetTransferMemberDetail.Permanent_Address2);
            $('#dateMemberShip').val(GetTransferMemberDetail.DateofMembership);
            $('#dateElectionMC').val(GetTransferMemberDetail.DateElectionMC);
            $('#DateofOfficeBearer').val(GetTransferMemberDetail.DateOfficeBearer);
            $('#DateofIssuanceAgenda').val(GetTransferMemberDetail.DateIssueAgenda);
            $('#Society_DateofResolution').val(GetTransferMemberDetail.DateofResolution);
            $('#boAboD').val(GetTransferMemberDetail.BOA_BOD);
            $('#IsAttendMeeting').val(GetTransferMemberDetail.IsAttendMeeting);
            $('#ShareMemberShipId').val(GetTransferMemberDetail.TransferorId);
            $('#Transferor_CommunityId').val(GetTransferMemberDetail.STR_CommunityTypeId);
            $('#ST_hiddenId').val("Yes");  
            $('#ST_IsEditCase').val("Yes");  
            $('#updatedId').val(GetTransferMemberDetail.UniqueId);
            $('#IdentityNo').val(IdentityNo);
            $('#TransfereeName').val(GetTransferMemberDetail.TransferorName);
            $('#Img_files_10').text("yes");
            $("#img").attr("src", GetTransferMemberDetail.baseConvert);
            $('#ImageEditVal').val(GetTransferMemberDetail.baseConvert); 
            $('#ImageEditName').val(GetTransferMemberDetail.ImageName); 
           // $("#img").attr("src", "data:image/png;base64," + GetTransferMemberDetail.baseConvert + "");
            var value = "";
            if ($('#CommunityofSociety').val() == 1) {
                value = "Employee";
            }
            else if ($('#CommunityofSociety').val() == 2) {
                value = "Defence/Ex Serviceman";
            }
            else if ($('#CommunityofSociety').val() == 3) {
                value = "General";
            }
            else if ($('#CommunityofSociety').val() == 4) {
                value = "SC";
            }
            else if ($('#CommunityofSociety').val() == 5) {
                value = "PH";
            }
            else if ($('#CommunityofSociety').val() == 6) {
                value = "EWS";
            }
            else if ($('#CommunityofSociety').val() == 7) {
                value = "Others";
            }
            sessionStorage.setItem("TypeId", value);
            $('#myModalApprovedLForm').modal('show');
            $('#shareTransModel').modal('show');
        },
        error: function (errormessage) {
        }
    });
}