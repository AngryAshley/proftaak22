<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous">
    <link rel="stylesheet" href="css/main.css">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>RailView</title>
</head>
<body>
    <nav class="navbar navbar-dark navbar-expand-lg bg-dark d-flex justify-content-center mb-3">
        <a class="navbar-brand" href="#">RailView</a>
    </nav>

    <div class="container">
        <div class="embed-responsive embed-responsive-21by9 d-flex justify-content-center mb-3">
            <!-- <iframe class="embed-responsive-item" id="test_frame" src="http://213.34.225.97:8080/mjpg/video.mjpg"></iframe> -->
            <iframe width="560" id="test_frame" class="embed-responsive-item" height="315" src="https://www.youtube.com/embed/4DXle0_Ud7s?&autoplay=1" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen autoplay></iframe>
        </div>

        <div class="row d-flex justify-content-center">
            <!-- <div class="col-sm w-100 d-flex justify-content-center">
                <button type="button" class="btn btn-secondary mr-5">Secondary</button>
                <p class="text-center">Eindhoven naar Helmond</p>
                <button type="button" class="btn btn-secondary ml-5">Secondary</button>
            </div>-->

            <div class="col-sm-auto">
                <button type="button" class="btn btn-danger mr-5">SEND ALERT!</button>
            </div>
            <div class="col-sm-auto mt-2">
                <p class="text-center">Eindhoven naar Helmond</p>
            </div>
            <div class="col-sm-auto">
                <button type="button" class="btn btn-secondary ml-5">FALSE ALARM</button>
            </div>
        </div>
    </div>
</body>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
    <script type="text/javascript" src="js/script.js" crossorigin="anonymous"></script>
    <script type="text/javascript" src="js/popupscript.js" crossorigin="anonymous"></script>
</html>