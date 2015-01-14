$(document).foundation();

$(document).ready(function () {
	loadGoogleMap();
	bindContactForm();
 });


function loadGoogleMap() {
	if ($("#map_canvas").length > 0) {
        var style = [{ "featureType": "landscape", "stylers": [{ "saturation": -100 }, { "lightness": 65 }, { "visibility": "on" }] }, { "featureType": "poi", "stylers": [{ "saturation": -100 }, { "lightness": 51 }, { "visibility": "simplified" }] }, { "featureType": "road.highway", "stylers": [{ "saturation": -100 }, { "visibility": "simplified" }] }, { "featureType": "road.arterial", "stylers": [{ "saturation": -100 }, { "lightness": 30 }, { "visibility": "on" }] }, { "featureType": "road.local", "stylers": [{ "saturation": -100 }, { "lightness": 40 }, { "visibility": "on" }] }, { "featureType": "transit", "stylers": [{ "saturation": -100 }, { "visibility": "simplified" }] }, { "featureType": "administrative.province", "stylers": [{ "visibility": "off" }] }, { "featureType": "water", "elementType": "labels", "stylers": [{ "visibility": "on" }, { "lightness": -25 }, { "saturation": -100 }] }, { "featureType": "water", "elementType": "geometry", "stylers": [{ "hue": "#ffff00" }, { "lightness": -25 }, { "saturation": -97 }] }];
        var latlng = new google.maps.LatLng(-34.922234, 138.625129);
        var settings = {
            zoom: 15,
            center: latlng,
            mapTypeControl: true,
            mapTypeControlOptions: { style: google.maps.MapTypeControlStyle.DROPDOWN_MENU },
            navigationControl: true,
            navigationControlOptions: { style: google.maps.NavigationControlStyle.SMALL },
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            styles: style
        };

        var map = new google.maps.Map(document.getElementById("map_canvas"), settings);
        var contentString = "<div class='margin-bottom' id='content'>" +
            "<h5>GretaKate Showroom</h5>" +
            "<p>3 Charles Street, <br/>Norwood South Australia 5067</p>" +
            "<div id='bodyContent'>" +
            "<a target='_blank' href='//www.google.com.au/maps/dir//3+Charles+St,+Norwood+SA+5067/@@-34.9223836,138.6229942,17z' title='Get Directions' class='ui-button'>Get Directions</a>" +
            "</div>" +
            "</div>";

        var infowindow = new google.maps.InfoWindow({ content: contentString });
        var companyMarker = new google.maps.Marker({
            position: latlng,
            map: map,
            title: "GretaKate"
        });

        google.maps.event.addListener(companyMarker, 'click', function () {
            infowindow.open(map, companyMarker);
        });
    }
}

function bindContactForm() {
	$("form").on('valid.fndtn.abide', function (e) {
            e.preventDefault();

            var form = $(this).closest("form");
            var model = {
                Name: $("input[name='Name']", form).val(),
                Email: $("input[name='Email']", form).val(),
                Message: $("textarea[name='Message']", form).val()
            };

            $.ajax({
                url: '/umbraco/surface/Enquiry/Submit/',
                type: 'POST',
                dataType: 'json',
                data: JSON.stringify(model),
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    if (data.IsSuccess) {
                        clearForm(form);
                        $('#form-success').foundation('reveal', 'open');
                    }
                    else {
                        alert("Failed " + data.Message);
                    }
                }
            });
        });

        $("form a.clear").click(function (e) {
            e.preventDefault();

            var form = $(this).closest("form");

            clearForm(form);
        });

        function clearForm(form) {
            $("input.text, textarea", form).each(function () {
                $(this).removeClass("error").val("");
            });

            $("label.error", form).each(function () {
                $(this).hide();
            });
        }
}