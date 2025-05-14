
function BindTransferMembership() {
    
    var e = document.getElementById("ShareMemberList");
    var SocietyTransID = e.options[e.selectedIndex].value;
    if (SocietyTransID === "") {
        alert("Kindly select society");
        //document.getElementById("btnForword").disabled = true;
    }
    else {
        loadSocietyDetails();
        loadForOfficer();
        loadForOfficer2();
        loadForOfficer3();
        loadForOfficer4();
    }

}
function loadSocietyDetails() {
    var e = document.getElementById("ShareMemberList");
    var SocietyTransID = e.options[e.selectedIndex].value;
    $.ajax({
        url: "/ARCS/getSocietyDetails/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: { SocietyTransID },
        success: function (result) {
            // alert(JSON.stringify(result))
            var getbySociety = result[0];
            //alert(getbySociety.DistrictForShowUSer);
            $('#NameofProposedSociety').val(getbySociety.SocietyName);
            $('#AreaofOperation').val(getbySociety.AreaofOperation);
            $('#ClassofSocietyandLiability').val(getbySociety.SocietyClassName);
            $('#SocietySubClassCode').val(getbySociety.SocietySubClassName);
            $('#RegisteredAddress').val(getbySociety.Address1);
            $('#HouseNoSectorNoRoad').val(getbySociety.Address2);
            $('#PostOffice').val(getbySociety.PostOffice);
            $('#PostalCode').val(getbySociety.Pin);
            //$('#AreaOfOperation').val(getbySociety.AreaOfOperation)
            $('#Mainobjects1').val(getbySociety.Mainobject1);
            $('#Mainobjects2').val(getbySociety.Mainobject2);
            $('#Mainobjects3').val(getbySociety.Mainobject3);
            $('#Mainobjects4').val(getbySociety.Mainobject4);
            $('#Noofmemberspresent').val(getbySociety.NoOfMembers);
            //$('#Occupation').val(getbySociety.OccupationName)
            $('#Categoryofsociety').val(getbySociety.CateOfSociety);
            $('#Estimatedunsecureddebtsofmembers').val(getbySociety.DebtsOfMembers);
            $('#AreaMortgagedbymembers').val(getbySociety.AreaMortgaged);
            $('#detailsofshares').val(getbySociety.DetailsOfShares);
            $('#Valueofashare').val(getbySociety.ValueOfShare);
            $('#ModeofPayment').val(getbySociety.ModeOfPayment);
            $('#Name1').val(getbySociety.Name1);
            $('#FatherName1').val(getbySociety.FatherName1);
            $('#Mobile1').val(getbySociety.Mobile1);
            $('#Email1').val(getbySociety.Email1);
            $('#Address3').val(getbySociety.Address3);
            $('#HouseNoSectorNoRoad1').val(getbySociety.HouseNoSectorNoRoad1);
            $('#PostOffice1').val(getbySociety.PostOffice1);
            $('#PostalCode1').val(getbySociety.PostalCode1);
            $('#DistrictForUser1').val(getbySociety.DistrictForShowUSer);
        },
        error: function (errormessage) {
            //alert(errormessage.responseText);
        }
    });
}
function loadForOfficer() {
    debugger;
    var e = document.getElementById("ShareMemberList");
    var SocietyTransID = e.options[e.selectedIndex].value;
    $.ajax({
        url: "/ShareTransfer/ShareMemberShipOfficer/",
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
                html += '<td><a id="ShareMembershipDetail" href="#" class="btn btn-block ink-reaction btn-flat btn-info" onclick="return GetbyOfficer(' + item.IdentityNo + ')">View</a></td > ';               
                html += '</tr>';
            });
            $('.tblShareMembershipOfficer').html(html);
        },
        error: function (errormessage) {
            //alert(errormessage.responseText);
        }
    });
}
function GetbyOfficer(IdentityNo) {
    $.ajax({
        url: "/ShareTransfer/GetbyShareTransferID/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: { IdentityNo },
        success: function (result) {
            var GetTransferMemberDetail = result;        
            $('#showName').val(GetTransferMemberDetail.MemberName);
            $('#FatherName').val(GetTransferMemberDetail.FatherName);
            $('#ManagingMemberRelationship').val(GetTransferMemberDetail.ManagingMemberRelationship);
            $('#GenderOfSocietyMember').val(GetTransferMemberDetail.Gender);
            $('#dateofbirth').val(GetTransferMemberDetail.Dob);
            $('#communityId').val(GetTransferMemberDetail.communityTypeId);
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
            $('#Present_PostalCodeOfSocietyMember').val(GetTransferMemberDetail.Pin);    
            $('#Present_Address2').val(GetTransferMemberDetail.Address2);    
            $('#P_Address1').val(GetTransferMemberDetail.Permanent_Address1);    
            $('#P_DistrictOfMember').val(GetTransferMemberDetail.Permanent_DistCode);    
            $('#P_PostOfficeOfSocietyMember').val(GetTransferMemberDetail.Permanent_PostOffice);    
            $('#P_PostalCodeOfSocietyMember').val(GetTransferMemberDetail.Permanent_Pin);    
            $('#P_Address2').val(GetTransferMemberDetail.Permanent_Address2);    

            $('#ShareMemberShipId').val(GetTransferMemberDetail.TransferorName);
            $('#dateMemberShip').val(GetTransferMemberDetail.DateofMembership);
            $('#dateElectionMC').val(GetTransferMemberDetail.DateElectionMC);
            $('#DateofOfficeBearer').val(GetTransferMemberDetail.DateOfficeBearer);
            $('#DateofIssuanceAgenda').val(GetTransferMemberDetail.DateIssueAgenda); 
            $('#Society_DateofResolution').val(GetTransferMemberDetail.DateofResolution);
            $('#boAboD').val(GetTransferMemberDetail.BOA_BOD);
            $('#IsAttendMeeting').val(GetTransferMemberDetail.IsAttendMeeting);          
            
            $('#myModalApprovedLForm').modal('show');
            $('#shareTransModel').modal('show');
        },
        error: function (errormessage) {
            //alert(errormessage.responseText);
        }

    });
    return false;
}
function loadForOfficer2() {
    var e = document.getElementById("ShareMemberList");
    var SocietyTransID = e.options[e.selectedIndex].value;
    $.ajax({
        url: "/ShareTransfer/ShareMemberShipOfficer/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: { SocietyTransID },
        success: function (result) {
            debugger;
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.MemberName + '</td>';
                html += '<td>' + item.TransferorName + '</td>';
                html += '<td>' + item.createddate + '</td>';
                html += '<td><a id="ShareMembershipDetail" href="#" class="btn btn-block ink-reaction btn-flat btn-info" onclick="return GetbyOfficer2(' + item.IdentityNo + ')">View Document</a></td > ';               
                html += '</tr>';
            });
            $('.tblShareMembershipOfficer2').html(html);
        },
        error: function (errormessage) {
            //alert(errormessage.responseText);
        }
    });
}
function GetbyOfficer2(IdentityNo) {
    $.ajax({
        url: "/ShareTransfer/GetbyShareTransferID/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: { IdentityNo },
        success: function (result) {
            var GetTransferMemberDetail = result;          
            $('#myModalDocument').modal('show');
        },
        error: function (errormessage) {
            //alert(errormessage.responseText);
        }

    });
    return false;
}
function loadForOfficer3() {
    var e = document.getElementById("ShareMemberList");
    var SocietyTransID = e.options[e.selectedIndex].value;
    $.ajax({
        url: "/ShareTransfer/ShareMemberShipOfficer/",
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
                html += '<td><a id="ShareMembeccrshipDetail" href="#" class="btn btn-block ink-reaction btn-flat btn-info" onclick="return ForwardMembershipOfficer(' + item.IdentityNo + ')">Proceed</a></td > ';
                html += '</tr>';
            });
            $('.tblShareMembershipOfficer3').html(html);
        },
        error: function (errormessage) {
            //alert(errormessage.responseText);
        }
    });
}
function loadForOfficer4() {
    var e = document.getElementById("ShareMemberList");
    var SocietyTransID = e.options[e.selectedIndex].value;
    $.ajax({
        url: "/ShareTransfer/ShareMemberShipOfficer/",
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
                html += '<td><a id="ShareMembeccrshipDetail" href="#" class="btn btn-block ink-reaction btn-flat btn-info" onclick="return BackToSociety(' + item.IdentityNo + ')">Send Back</a></td > ';
                html += '</tr>';
            });
            $('.tblShareMembershipOfficer4').html(html);
        },
        error: function (errormessage) {
            //alert(errormessage.responseText);
        }
    });
}
function ForwardMembershipOfficer(IdentityNo) {   
        var objMFD = {
            IdentityNo: IdentityNo
        };
    ST_loadremark(IdentityNo);
        $.ajax({
            url: "/ShareTransfer/TransfereeId",
            data: JSON.stringify(objMFD),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $('#myModalPopup').modal('show');
                //alert("Share Transfer has been Successfully Forwarded To ARCS Officer.")
                loadForOfficer();
            },
            error: function (errormessage) {
                //alert(errormessage.responseText);
            }
        });
}
function BackToSociety(IdentityNo) {
    var ans = confirm("Are You Sure You Want To Send Back to Society?");
    if (ans) {
        var objMFD = {
            IdentityNo: IdentityNo
        };
        $.ajax({
            url: "/ShareTransfer/SendBackToSociety",
            data: JSON.stringify(objMFD),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                alert("Share Transfer has been Sent Back To Society.")
                loadForOfficer();
            },
            error: function (errormessage) {
                //alert(errormessage.responseText);
            }
        });
    }
}
function ST_loadremark() {
    var e = document.getElementById("ShareMemberList");
    var SocietyTransID = e.options[e.selectedIndex].value;
    $.ajax({
        url: "/ShareTransfer/ShareTransferRemark/",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: { SocietyTransID },
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                var getDate = item.Date
                var date = new Date(parseInt(getDate.substr(6)));
                var month = date.getMonth() + 1;
                html += '<tr>';
                html += '<td>' + item.Remarks + '</td>';
                html += '<td>' + date.getDate() + '/' + month + '/' + date.getFullYear() + '</td>';
                html += '</tr>';
            });
            $('.ST_tblBindRemark').html(html);
        },
        error: function (errormessage) {
            //alert(errormessage.responseText);
        }
    });
}
