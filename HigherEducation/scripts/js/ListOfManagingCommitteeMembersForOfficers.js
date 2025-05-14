//Load Data in Table when documents is ready
function BindMovedSociety() {
    var e = document.getElementById("InspectorList");
    var SocietyTransID = e.options[e.selectedIndex].value;
    if (SocietyTransID === "") {
        alert("Kindly Select Inspector First");      
    }
    else {
        loadMovedSociety();        
    }
}
function loadMovedSociety() {
    var e = document.getElementById("InspectorList");
    var InspectorId = e.options[e.selectedIndex].value;
    $.ajax({
        url: "/ARCS/BindMoveData/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: { InspectorId},
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td hidden="hidden">' + item.value + '</td>';
                //html += '<td><a id="MembersDetails" href="#" class="btn btn-block ink-reaction btn-flat btn-info" onclick="return getMoveByID(' + item.Value + ')">' + item.Text + '</a>';               
                html += '<td>' + item.Text + '</td>';
                html += '<td>' + item.Value + '</td>';                
                //html += '<td><a id="MembersDetails" href="#" class="btn btn-block ink-reaction btn-flat btn-info" onclick="return getMoveByID(' + item.Value + ')">View</a>';               
            });
            if (html == "") {
                alert("No Records Found");
            }
            $('.tblBindMoveData').html(html);
            $('#mylist').trigger('click');         
        },
        error: function (errormessage) {
            //alert(errormessage.responseText);
        }
    });
}

function getMoveByID(SocietyTransID) {
    window.location.href = 'http://localhost:62749/BackLogOfficer/MoveToInspector?Value=BackLog';
}


function BindAllSocietyDetailsForOfficers() {
    var e = document.getElementById("SocietyList");
    var SocietyTransID = e.options[e.selectedIndex].value;
    $("#CurrentSocietyId").val(SocietyTransID);
    if (SocietyTransID === "") {
        alert("Kindly select society");      
        document.getElementById("btnSendBack").disabled = true;
        document.getElementById("btnForword").disabled = true;
    }
    else {
        loadSocietyDetails();
        loadManagingCommitteeMembers();
        loadSocietyMembers();
        document.getElementById("btnSendBack").disabled = false;
        document.getElementById("btnForword").disabled = false;
    }
}

function BindAllSocietyDetailsForOfficersForApproved() {
    var SocietyTransID = $('#txtSocietyTransId').val();
    if (SocietyTransID === "") {
        alert("Kindly select society");
    }
    else {
        loadSocietyDetailsForApproved();
        loadManagingCommitteeMembersForApproved();
        loadSocietyMembersForApproved();
    }
}

function loadSocietyDetailsForApproved() {
    var SocietyTransID = $('#txtSocietyTransId').val();
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

function loadManagingCommitteeMembersForApproved() {
    var SocietyTransID = $('#txtSocietyTransId').val();
    $.ajax({
        url: "/ARCS/ManagingCommitteMembersList/",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: { SocietyTransID },
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td hidden="hidden">' + item.SocietyMemberID + '</td>';
                html += '<td>' + item.SocietyMemberName + '</td>';
                html += '<td>' + item.SocietyMemberDesignationName + '</td>';
                html += '<td>' + item.RelationshipMemberName + '</td>';
                html += '<td>' + item.RelationshipName + '</td>';
                html += '<td><a id="btnCommitteMembers" href="#" class="btn btn-block ink-reaction btn-flat btn-info" onclick="return getbySocietyManagingCommittMembers(' + item.SocietyMemberID + ')">View</a> ';
                html += '</tr>';
            });
            $('.tblBindCommitteMembers').html(html);
        },
        error: function (errormessage) {
            //alert(errormessage.responseText);
        }
    });
}

function loadSocietyMembersForApproved() {
    var SocietyTransID = $('#txtSocietyTransId').val();
    $.ajax({
        url: "/ARCS/SocietyMembersList/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: { SocietyTransID },
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td hidden="hidden">' + item.MemberSNo + '</td>';
                html += '<td>' + item.MemberName + '</td>';
                html += '<td>' + item.MemberDesignationName + '</td>';
                html += '<td>' + item.FatherName + '</td>';
                html += '<td>' + item.Gender + '</td>';
                html += '<td>' + item.Age + '</td>';
                html += '<td>' + item.Mobile + '</td>';
                html += '<td><a id="MembersDetails" href="#" class="btn btn-block ink-reaction btn-flat btn-info" onclick="return getbySocietyMemberID(' + item.MemberSNo + ')">View</a>';
                html += '</tr>';
            });
            $('.tblBindMembers').html(html);
        },
        error: function (errormessage) {
            //alert(errormessage.responseText);
        }
    });
}

function loadSocietyDetails() {
    var e = document.getElementById("SocietyList");
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

function loadManagingCommitteeMembers() {
    var e = document.getElementById("SocietyList");
    var SocietyTransID = e.options[e.selectedIndex].value;
    $.ajax({
        url: "/ARCS/ManagingCommitteMembersList/",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: { SocietyTransID },
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td hidden="hidden">' + item.SocietyMemberID + '</td>';
                html += '<td>' + item.SocietyMemberName + '</td>';
                html += '<td>' + item.SocietyMemberDesignationName + '</td>';
                html += '<td>' + item.RelationshipMemberName + '</td>';
                html += '<td>' + item.RelationshipName + '</td>';
                html += '<td><a id="btnCommitteMembers" href="#" class="btn btn-block ink-reaction btn-flat btn-info" onclick="return getbySocietyManagingCommittMembers(' + item.SocietyMemberID + ')">View</a> ';
                html += '</tr>';
            });
            $('.tblBindCommitteMembers').html(html);
        },
        error: function (errormessage) {
            //alert(errormessage.responseText);
        }
    });
}

function loadSocietyMembers() {
    var e = document.getElementById("SocietyList");
    var SocietyTransID = e.options[e.selectedIndex].value;
    $.ajax({
        url: "/ARCS/SocietyMembersList/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: { SocietyTransID },
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td hidden="hidden">' + item.MemberSNo + '</td>';
                html += '<td>' + item.MemberName + '</td>';
                html += '<td>' + item.MemberDesignationName + '</td>';
                html += '<td>' + item.FatherName + '</td>';
                html += '<td>' + item.Gender + '</td>';
                html += '<td>' + item.Age + '</td>';
                html += '<td>' + item.Mobile + '</td>';
                html += '<td><a id="MembersDetails" href="#" class="btn btn-block ink-reaction btn-flat btn-info" onclick="return getbySocietyMemberID(' + item.MemberSNo + ')">View</a>';
                html += '</tr>';
            });
            $('.tblBindMembers').html(html);
        },
        error: function (errormessage) {
            //alert(errormessage.responseText);
        }
    });
}

function Download() {
    $.ajax({
        url: "/ARCS/downLoadLForm/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

        },
        error: function (errormessage) {
            //alert(errormessage.responseText);
        }
    });
    return false;
}

function getbySocietyMemberID(MemberSNo) {
    $.ajax({
        url: "/ARCS/getbySocietyMemberID/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: { MemberSNo },
        success: function (result) {
            //alert(JSON.stringify(result));
            var getbySocietyMemberID = result;
            $('#MemberSNo').val(MemberSNo);
            $('#MemberName').val(getbySocietyMemberID.MemberName);
            $('#FatherName').val(getbySocietyMemberID.FatherName);
            $('#GenderOfSocietyMember').val(getbySocietyMemberID.Gender);
            $('#Age').val(getbySocietyMemberID.Age);
            $('#OccupationOfMember').val(getbySocietyMemberID.OccupationVal);
            $('#Address1').val(getbySocietyMemberID.Address1);
            $('#Address2').val(getbySocietyMemberID.Address2);
            $('#PostOfficeOfSocietyMember').val(getbySocietyMemberID.PostOffice);
            $('#PostalCodeOfSocietyMember').val(getbySocietyMemberID.Pin);
            $('#DistrictOfMember').val(getbySocietyMemberID.DistrictName);
            $('#NoofSharesSubscribed').val(getbySocietyMemberID.NoOfShares);
            $('#NameofNominee').val(getbySocietyMemberID.NomineeName);            
            $('#NomineeAge').val(getbySocietyMemberID.NomineeAge);
            $('#RelationshipCodeOfSocietyMember').val(getbySocietyMemberID.RelationshipName);
            $('#RelationwithMember').val(getbySocietyMemberID.RelationOfMemberName);
            $('#MobileNumberOfSocietyMember').val(getbySocietyMemberID.Mobile);
            $('#AadharNo1').val(getbySocietyMemberID.AadharNo);
            $('#EmailId').val(getbySocietyMemberID.EmailId);
            $('#myModal1').modal('show');
        },
        error: function (errormessage) {
            
        }
    });
    return false;
}

function getbySocietyManagingCommittMembers(SocietyMemberID) {
    $.ajax({
        url: "/ARCS/getbyManagingCommitteeMemberID/",
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: { SocietyMemberID },
        success: function (result) {
            
            var getbySocietyMemberID = result[0];
            $('#SocietyMemberID').val(SocietyMemberID);
            $('#SocietyMemberName').val(getbySocietyMemberID.SocietyMemberName);
            $('#SocietyMemberDesignation').val(getbySocietyMemberID.SocietyMemberDesignationName);
            $('#RelationshipCode').val(getbySocietyMemberID.RelationshipName);
            $('#RelationshipMemberName').val(getbySocietyMemberID.RelationshipMemberName);
            $('#Address').val(getbySocietyMemberID.Address);
            $('#HouseNo').val(getbySocietyMemberID.HouseNo);
            $('#SectorStreet').val(getbySocietyMemberID.SectorStreet);
            $('#CommitteMemberDistrict').val(getbySocietyMemberID.DistrictName);
            $('#MobileNumber').val(getbySocietyMemberID.MobileNumber);
            $('#Email').val(getbySocietyMemberID.Email);
            $('#AadharNo').val(getbySocietyMemberID.AadharNo);
            $('#IsPresident').val(getbySocietyMemberID.IsPresident);
            $('#myModal').modal('show');
        },
        error: function (errormessage) {
            
        }
    });
    return false;
}

