//parameters needs to be an object



function keyWordSearch(){
    var encodedParameters;



    if ($('#search-input').val() != "") {
        encodedParameters = handleParameters();
    }
    var url= "http://api.indeed.com/ads/apisearch?publisher=6943012943597582&format=json&" + encodedParameters + "&sort=date&radius=&st=&jt=&start=&limit=50&end=50&fromage=&filter=&latlong=1&co=us&chnl=&userip=1.2.3.4&useragent=Mozilla/%2F4.0%28Firefox%29&v=2";
    $('head').append('<script src="' + url + '"></script>');

}

function handleParameters(){
    var searchInput = $('#search-input').val();
    var filterInput = $('#city-state-input').val();

    var parameters = {
        q: searchInput,
        l: filterInput,
        callback: 'handleReturnResults'
    }

//can do a bunch of ifs for different filter input

  parameters = encode(parameters);
  return parameters;
}


function encode(parameters) {
  var encodedInput = '';

  for (var key in parameters) {
    encodedInput += encodeURIComponent(key) + '=' + encodeURIComponent(parameters[key]) + '&';
  }
  if (encodedInput.length != 0) {
    encodedInput = encodedInput.slice(0,  -1);
  }

  return encodedInput;
}


function handleReturnResults(totalResults) {
  console.log(totalResults);
  var rawResults = totalResults.results;
  var readyResults = [];

  for (var i = 0; i < rawResults.length; i++) {
    var currentItem = rawResults[i];
    var individualResult = {
        jobTitle : currentItem.jobtitle,
        url : currentItem.url,
        company : currentItem.company,
        latitude : currentItem.latitude,
        longitude : currentItem.longitude,
        postDate : currentItem.date,
        city : currentItem.city,
        state : currentItem.state
    };
    readyResults[i] = individualResult;

  }

  if (readyResults.length != 0) {
      $('#result').show();
      $('#display-message').hide();
      $('#save').show();
      $('#resultHeader').show();
      displayResultsInHtml(readyResults);
    }
  else {
      $('#result').hide();
      $('#save').hide();
      $('#display-message').show();
      $('#resultHeader').hide();
      $('#display-message').html("I'm sorry, your search yielded no results. Please try again with other keywords.");
    }
}



function displayResultsInHtml(readyResults){
    var htmlToAdd = "";
    readyResults = readyResults.forEach(getDisplayForTrack);

    function getDisplayForTrack(individual) {

        htmlToAdd += '<div class="col-sm-1"><center> <input checked="unchecked" id="isSaved" type="checkbox" name="checkbox" value="{0}"/>'.replace("{0}", individual.latitude + "*" + individual.longitude + "*" + individual.company + "*" + individual.postDate + "*" + individual.city + "*" + individual.state);
        htmlToAdd += '</center></div><div class="col-sm-3"><center><a href="urlLink" target="_blank" id="firstA">'.replace("urlLink", individual.url) + 'title'.replace("title", individual.jobTitle) + '</a></div>';
        htmlToAdd += '<div class="col-sm-3"><center>'+ individual.postDate + '</center></div>'
        htmlToAdd += '<div class="col-sm-3"><center>' + individual.company + '</center></div>'
        htmlToAdd += '<div class="col-sm-2"><center><form action="/Jobs/Details" method = "post"><input type="text" name="Latitude" value = "latitude" hidden><input type="text" name="Longitude" value = "longitude" hidden><input type="text" name="City" value = "city" hidden><input type="submit" value = "city2">'   
            .replace("longitude", individual.longitude)
            .replace("latitude", individual.latitude)
            .replace("city", individual.city)
            .replace("city2", individual.city) + '</a></form></center></div><div class="row"><div class="col-sm-12"><hr /></div></div>';


        //htmlToAdd += '<div> <input checked="unchecked" id="isSaved" type="checkbox" name="checkbox" value="{0}"/>'.replace("{0}", individual.latitude + "*" + individual.longitude + "*" + individual.company + "*" + individual.postDate + "*" + individual.city + "*" + individual.state);     
        //htmlToAdd += '<a href="urlLink" target="_blank" id="firstA">'.replace("urlLink", individual.url) + 'title'.replace("title", individual.jobTitle) + '</a></div>';      
        //htmlToAdd += '<form action="/Jobs/Details" method = "post"><input type="text" name="Latitude" value = "latitude" hidden><input type="text" name="Longitude" value = "longitude" hidden><input type="text" name="City" value = "city" hidden><input type="submit" value = "city2">'
        //    .replace("longitude", individual.longitude)
        //    .replace("latitude", individual.latitude)
        //    .replace("city", individual.city)
        //    .replace("city2", individual.city) + '</form>';
    }

    $('#result').html(htmlToAdd);
}


function sortBySortInput(readyResults, typeChoice)
{
  if($('#sort-choice').val() == "noFilter"){
    typeChoice = $('#filter').val();
  }

  sortedResults = readyResults.sort(compare);
  return sortedResults;

  function compare(a, b){

    if (a[typeChoice] < b[typeChoice])
      return -1;
    if (a[typeChoice] > b[typeChoice])
      return 1;
    return 0;
  }
}

function getCheckedBoxes(checkbox) {
 
    var checkBoxes = $("#result div #isSaved");
    var isChecked = checkBoxes.map((x) => { return checkBoxes[x].checked });
    var links = $("[id=firstA]");

    var jobs = links.map((x) => { return { 'JobTitle': links[x].text, 'Link': links[x].href, 'Information' : checkBoxes[x].defaultValue }});
    var checkedJobs = [];
    for (var i = 0; i < isChecked.length; i++) {
        if (isChecked[i]) {
            checkedJobs.push(jobs[i])
        }
    }
    $.ajax({
        type: "POST",
        url: "http://localhost:44172/Jobs/SaveJobs",
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(checkedJobs)
    });
}

function goToCity(lat, lng) {
    var coordinates = lat + "," + lng;
    var currentUrl = 'https://api.teleport.org/api/locations/' + coordinates + '/?embed=location%3Anearest-cities%2Flocation%3Anearest-city';
    var cityNameFull = "";
    var cityNameShort = "";
    var cityPopulation = "";
    var cityDescription = "";
    var urbanAreaURL = "";
    var html = "";
        
    $.ajax
    ({
        type: "GET",
        url: currentUrl,
        //dataType: "json",
        success: function (response) {
            cityNameFull = response._embedded["location:nearest-cities"][0]._embedded["location:nearest-city"].full_name;
            cityNameShort = response._embedded["location:nearest-cities"][0]._embedded["location:nearest-city"].name;
            cityPopulation = response._embedded["location:nearest-cities"][0]._embedded["location:nearest-city"].population;
            try{
                urbanAreaURL = response._embedded["location:nearest-cities"][0]._embedded["location:nearest-city"]._links["city:urban_area"].href + "scores/";
            } catch (error) {

            }
            var html = '<div id="city"><h3>{0}</h3></div>'.replace("{0}", cityNameFull);

            $('#cityName').html(html);

            html = '<div id="population"><h4>Population: {0}</h4></div>'.replace("{0}", cityPopulation);

            $('#populationField').html(html);
            
            SetDescription(urbanAreaURL);

       }
    }
    );
}
function SetDescription(URL) {
    var citySummary = "";
    var html = "";
    var currentUrl2 = URL;

    $.ajax
        ({
            type: "GET",
            url: currentUrl2,
            //dataType: "jsonp",
            success: function (response) {
                citySummary = '<div id="summaryText">{0}</div>'.replace("{0}", response.summary);
                for (var i = 0; i < response.categories.length; i++) {
                    html += '<div>';
                    html += '<strong>{0}</strong> : Rated {2} out of 10'.replace("{0}", response.categories[i].name).replace("{2}", Math.max(Math.round(response.categories[i].score_out_of_10  * 10) / 10, 1).toFixed(1));
                    html += '<br />';
                    html += '<progress value="{1}" max="100"></progress>'.replace("{1}", response.categories[i].score_out_of_10 * 10);
                    html += '</div>';
                }
                $('#cityInfo').html(citySummary);
                $('#progress').html(html);
            }
        }
        );
}

function saveJob() {
    var checkedBoxes = getCheckedBoxes("checkbox");
}

'<div class="progress-bar progress-bar-success progress-bar-striped" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width:40%">40% Complete (success)</div>'
