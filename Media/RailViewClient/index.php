<?php include 'includes/layout.php' ?>

<div class="row d-flex justify-content-md-around">
    <div class="col-3 notifications">
        <h4>Meldingen</h4>
        <div id="log" class="border border-dark border-2 notifications">
            <!-- Template log -->
        </div>

        <input type="button" class="btn btn-success my-2" id="btnpopup" value="Bel 112" onclick="ShowPopUp();" />
        <input type="button" class="btn btn-success my-2" id="liveToastBtn" value="Test" onclick="ShowToast();" />
        <input type="button" class="btn btn-success my-2" id="btntrain" value="Hide Active Trains" onclick="ShowAndHideTrains();" />
    </div>
    <div class="col-9 map-style">
        <div id="map" class="map-style"></div>
    </div>
</div>
<div class="row">
    <div id="notification-area">
        <!--Notifications-->
    </div>
</div>

<template id="all-data-template">
    {{#.}}
    <div class="text-center border-top border-dark border-2">
        <p class="px-2 pt-2 text-capitalize">
            {{Times}} <br /> {{Route}} <br /> <b>{{Alert}}</b>
        </p>
    </div>
    {{/.}}
</template>

<?php include 'includes/layout_bottom.php' ?>
