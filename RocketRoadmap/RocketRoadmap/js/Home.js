


function deleteModal()
{
    PageMethods.DeleteRoadmap(document.getElementById("deleteModalTitle").getAttribute("modalName"));
    console.log("Got here");
    location.reload();
}

function AreYouSure(name)
{

    $("#DeleteModal").modal("show");
    document.getElementById("deleteModalTitle").setAttribute("modalName", name);

}