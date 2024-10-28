$(document).ready(function () {
    GetRemediationCounts();
});
function GetRemediationCounts() {
    $.ajax({
        type: "GET",
        url: getRemediationCountsURL,
        //data: counts,
        
        success: function (data) {
            if (Number(data.organizationParentCount) > 0) {
                SetRemediationCount($('#OrganizationParentCount'), data.organizationParentCount)
                $('#OrganizationTableSection').removeClass("d-none");

            }
            else {
                $('#ZeroInboxOrganizationSection').removeClass("d-none");
            }
            if (Number(data.officeLocationCount) > 0) {
                SetRemediationCount($('#OfficeLocationCount'), data.officeLocationCount)
                $('#OfficeLocationTableSection').removeClass("d-none");

            }
            else {
                $('#ZeroInboxOfficeLocationSection').removeClass("d-none");
            }
            if (Number(data.organizationalUnitCount) > 0) {
                SetRemediationCount($('#OrganizationalUnitCount'), data.organizationalUnitCount)
                $('#OrgUnitTableSection').removeClass("d-none");
            }
            else {
                $('#ZeroInboxOrgUnitSection').removeClass("d-none");
            }
            if (Number(data.academicCatalogCount) > 0) {
                SetRemediationCount($('#AcademicCatalogCount'), data.academicCatalogCount)
                $('#AcademicCatTableSection').removeClass("d-none");
            }
            else {
                $('#ZeroInboxAcademicCatSection').removeClass("d-none");
            }
            if (Number(data.employeeCount) > 0) {
                SetRemediationCount($('#EmployeeCount'), data.employeeCount)
                var employeePage = $('#EmployeeTableSection').val();
                if (employeePage != undefined) {
                    LoadEmployeeList();
                }
                $('#EmployeeTableSection').removeClass("d-none");
            }
            else {
                $('#ZeroInboxEmployeeSection').removeClass("d-none");
            }
            if (Number(data.financeParentCount) > 0) {
                SetRemediationCount($('#FinanceParentCount'), data.financeParentCount)
            }
            if (Number(data.designationCount) > 0) {
                SetRemediationCount($('#DesignationCount'), data.designationCount)
                $('#DesignationTableSection').removeClass("d-none");

            }
            else {
                $('#ZeroInboxDesignationSection').removeClass("d-none");
            }
            if (Number(data.giftTransmittalCount) > 0) {
                SetRemediationCount($('#GiftTransmittalCount'), data.giftTransmittalCount)
            }
            if (Number(data.uafCount) > 0) {
                SetRemediationCount($('#UafCount'), data.uafCount)
            }
            if (Number(data.uaCount) > 0) {
                SetRemediationCount($('#UaCount'), data.uaCount)
            }
            if (Number(data.initializedCount) > 0) {
                SetRemediationCount($('#InitializedCount'), data.initializedCount)
            }
            if (Number(data.waitingForBursarCount) > 0) {
                SetRemediationCount($('#WaitingForBursarCount'), data.waitingForBursarCount)
            }
            if (Number(data.waitingForPreparerCount) > 0) {
                SetRemediationCount($('#WaitingForPreparerCount'), data.waitingForPreparerCount)
            }

            if (Number(data.secondaryCount) > 0) {
                SetRemediationCount($('#SecondaryApproverCount'), data.secondaryCount)
            }

            if (Number(data.gtFinalizeCount) > 0) {
                SetRemediationCount($('#GTFinalizeCount'), data.gtFinalizeCount)
            }

            if (Number(data.guFinalizeCount) > 0) {
                SetRemediationCount($('#GUFinalizeCount'), data.guFinalizeCount)
            }

            if (Number(data.disbursementsCount) > 0) {
                SetRemediationCount($('#DisbursementCount'), data.disbursementsCount)
            }
            if (Number(data.etDisbursementCount) > 0) {
                SetRemediationCount($('#ETDisbursementCount'), data.etDisbursementCount)
            }
            if (Number(data.stDisbursementCount) > 0) {
                SetRemediationCount($('#STDisbursementCount'), data.stDisbursementCount)
            }
            if (Number(data.emDisbursementCount) > 0) {
                SetRemediationCount($('#EMDisbursementCount'), data.emDisbursementCount)
            }
            if (Number(data.readyForProcessingDisbursementCount) > 0) {
                SetRemediationCount($('#ReadyForProcessingDisbursementCount'), data.readyForProcessingDisbursementCount)
            }

            if (Number(data.newVendorCount) > 0) {
                SetRemediationCount($('#NewVendorCount'), data.newVendorCount)
            }

            if (Number(data.fundsTransferTotalCount) > 0) {
                SetRemediationCount($('#FundsTransferCount'), data.fundsTransferTotalCount)
            }

            if (Number(data.unroutedFundsTransferCount) > 0) {
                SetRemediationCount($('#UnroutedFundsTransferCount'), data.unroutedFundsTransferCount)
            }

            if (Number(data.unrestrictedFundsTransferCount) > 0) {
                SetRemediationCount($('#UnrestrictedFundsTransferCount'), data.unrestrictedFundsTransferCount)
            }

            if (Number(data.restrictedFundsTransferCount) > 0) {
                SetRemediationCount($('#RestrictedFundsTransferCount'), data.restrictedFundsTransferCount)
            }

            if (Number(data.endowmentFundsTransferCount) > 0) {
                SetRemediationCount($('#EndowmentFundsTransferCount'), data.endowmentFundsTransferCount)
            }

            if (Number(data.giftFundsTransferCount) > 0) {
                SetRemediationCount($('#GiftFundsTransferCount'), data.giftFundsTransferCount)
            }

            if (Number(data.generalCounselFundsTansferCount) > 0) {
                SetRemediationCount($('#GeneralCounselFundsTansferCount'), data.generalCounselFundsTansferCount)
            }

            if (Number(data.readyForProcessingFundsTransferCount) > 0) {
                SetRemediationCount($('#ReadyForProcessingFundsTransfersCount'), data.readyForProcessingFundsTransferCount)
            }

            if (Number(data.studentCount) > 0) {
                SetRemediationCount($('#StudentCount'), data.studentCount)
                $('#StudentTableSection').removeClass("d-none");
            }
            else {
                $('#ZeroInboxStudentSection').removeClass("d-none");
            }
            if (Number(data.studentBioDemCount) > 0) {
                SetRemediationCount($('#StudentBioDemCount'), data.studentBioDemCount)
                var studentBioDemPage = $('#StudentTableSection').val();
                if (studentBioDemPage != undefined) {
                    LoadStudentList();
                }

                $('#StudentTableSection').removeClass("d-none");
            }
            else {
                $('#ZeroInboxStudentSection').removeClass("d-none");
            }
            if (Number(data.studentEnrollmentCount) > 0) {
                SetRemediationCount($('#StudentEnrollmentCount'), data.studentEnrollmentCount)
                var studentEnrollmentPage = $('#StudentEnrollmentTableSection').val();
                if (studentEnrollmentPage != undefined) {
                    LoadStudentEnrollmentList();
                }
                $('#StudentEnrollmentTableSection').removeClass("d-none");
                
            }
            else {
                $('#ZeroInboxStudentEnrollmentSection').removeClass("d-none");
            }
            if (Number(data.studentDegreeCount) > 0) {
                SetRemediationCount($('#StudentDegreeCount'), data.studentDegreeCount)
                var studentDegreeistPage = $('#StudentDegreeTableSection').val();
                if (studentDegreeistPage != undefined) {
                    LoadStudentDegreeList();
                }
                $('#StudentDegreeTableSection').removeClass("d-none");
            }
            else {
                $('#ZeroInboxStudentDegreeSection').removeClass("d-none");
            }
            if (Number(data.studentAcademicPlanCount) > 0) {
                SetRemediationCount($('#StudentAcademicPlanCount'), data.studentAcademicPlanCount)
                var studentAcademicPlanListPage = $('#StudentAcademicPlanTableSection').val();
                if (studentAcademicPlanListPage != undefined) {
                    LoadStudentAcademicPlanList();
                }
                $('#StudentAcademicPlanTableSection').removeClass("d-none");
            }
            else {
                $('#ZeroInboxStudentAcademicPlanSection').removeClass("d-none");
            }
            if (Number(data.studentAcademicInvolvementCount) > 0) {
                SetRemediationCount($('#StudentAcademicInvolvementCount'), data.studentAcademicInvolvementCount)
                var studentAcademicInvolvementListPage = $('#StudentAcademicInvolvementTableSection').val();
                if (studentAcademicInvolvementListPage != undefined) {
                    LoadStudentAcademicInvolvementList();
                }
                $('#StudentAcademicInvolvementTableSection').removeClass("d-none");
            }
            else {
                $('#ZeroInboxStudentAcademicInvolvementSection').removeClass("d-none");
            }
            if (Number(data.studentContactCount) > 0) {
                SetRemediationCount($('#StudentContactCount'), data.studentContactCount)
                $('#ConstiutientTableSection').removeClass("d-none");
            }
            else {
                $('#ZeroInboxSection').removeClass("d-none");
            }
            if (Number(data.studentParentCount) > 0) {
                SetRemediationCount($('#StudentParentCount'), data.studentParentCount)
                var studentParentListPage = $('#StudentParentTableSection').val();
                if (studentParentListPage != undefined) {
                    LoadStudentParentList();
                }
                $('#StudentParentTableSection').removeClass("d-none");
            }
            else {
                $('#ZeroInboxStudentParentSection').removeClass("d-none");
            }
            if (Number(data.constituentCount) > 0) {
                SetRemediationCount($('#ConstituentCount'), data.constituentCount)
                $('#ConstiutientTableSection').removeClass("d-none");
            }
            else {
                $('#ZeroInboxSection').removeClass("d-none");
            }
            if (Number(data.constituentIndividualCount) > 0) {
                SetRemediationCount($('#ConstituentIndividualCount'), data.constituentIndividualCount)
                var constituentListPage = $('#ConstituentIndividualTableSection').val();
                if (constituentListPage != undefined) {
                    LoadConstituentList();
                }
                $('#ConstituentIndividualTableSection').removeClass("d-none");
            }
            else {
                $('#ZeroInboxConstituentIndividualSection').removeClass("d-none");
            }
            console.log(data.constituentPhoneCount);
            if (Number(data.constituentPhoneCount) > 0) {
                SetRemediationCount($('#ConsituentPhoneCount'), data.constituentPhoneCount)
                var constituentPhoneListPage = $('#ConstiutientPhoneTableSection').val();
                if (constituentPhoneListPage != undefined) {
                    LoadConstituentPhoneList();
                }
                $('#ConstiutientPhoneTableSection').removeClass("d-none");
            }
            else {
                $('#ZeroInboxConstituentPhoneSection').removeClass("d-none");
            }
            if (Number(data.constituentEmailCount) > 0) {
                SetRemediationCount($('#ConstituentEmailCount'), data.constituentEmailCount)
                var constituentEmailListPage = $('#ConstiutientEmailTableSection').val();
                if (constituentEmailListPage != undefined) {
                    LoadConstituentEmailList();
                }
                $('#ConstiutientEmailTableSection').removeClass("d-none");
            }
            else {
                $('#ZeroInboxConstituentEmailSection').removeClass("d-none");
            }
            if (Number(data.constituentAddressCount) > 0) {
                SetRemediationCount($('#ConstituentAddressCount'), data.constituentAddressCount)
                $('#ConstiutientAddressTableSection').removeClass("d-none");
            }
            else {
                $('#ZeroInboxConstituentAddressSection').removeClass("d-none");
            }
            //ADD STUDENT SCHOLARSHIP 
            //ADD STUDENT ACADEMIC CAT 
            //ConstituentGroupList Coming Soon...
            //if (Number(data.constituentCount) > 0) {
            //    SetRemediationCount($('#constituentCount'), data.constituentCount)
            //}

            //ConstituentOrganizationList Coming Soon...

            //if (Number(data.constituentCount) > 0) {
            //    SetRemediationCount($('#constituentCount'), data.constituentCount)
            //}


        },
        error: function (request, error) {
            console.log(error);
            alert(error);
        }
    });
}

function SetRemediationCount(sectionElem, remediationVal) {
    //find the equivalant of string.format in jquery
    var formattedVal = remediationVal.toLocaleString("en-US");
    var childElem = `<span class="badge badge-error">${formattedVal}</span>`

    $(sectionElem).html(childElem);

}