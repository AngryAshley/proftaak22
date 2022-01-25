<?php include 'includes/layout.php' ?>

<div class="embed-responsive embed-responsive-21by9 d-flex justify-content-center mb-3">
    <!-- <iframe class="embed-responsive-item video-stream " src="https://www.youtube.com/embed/FfEJhEVcK4Q?&autoplay=1" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen autoplay></iframe> -->
    <!-- <iframe class="embed-responsive-item video-stream " src="https://192.168.161.205/000001.jpg" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen autoplay></iframe> -->
    <img src="https://192.168.161.205/000001.jpg" id="testid">
</div>

<div class="row d-flex justify-content-between">

    <div class="col">
        <button onclick="EmergencyAlarm()" type="button" class="btn btn-danger mr-5">SEND ALERT!</button>
    </div>
    <div class="col mt-2">
        <p class="text-center"><strong>Eindhoven naar Helmond</strong></p>
    </div>
    <div class="col">
        <button onclick="FalseAlarm()" type="button" class="btn btn-success ml-5 float-right">FALSE ALARM</button>
    </div>
</div>

<?php include 'includes/layout_bottom.php' ?>