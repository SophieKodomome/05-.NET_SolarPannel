<h1>Solar Pannel</h1>
<h2>Goal:</h2>
<p>A webApp in which clients have the ability to list their electrical expenses (in Watt) and given the suitable solar panel and battery fitting their consumption</p>
<h2>Features:</h2>
<ul>
    <li>Register a house and all its electrical devices</li>
    <li>Have a list of all houses and their electrical devices</li>
    <li>Being Able to change said house electrical devices and hourly consumption</li>
    <li>Have a list of solar panel's efficiency rate by hour and semestrial period</li>
    <li>Have a list of battery</li>
</ul>
<h2>Technology &#x1F680;</h2>
    <li>.NET</li>
    <li>Tailwind</li>
    <li>Postgres</li>
<h2>To do list &#x1F4CB;</h2>
    <h3>Page Index:</h3>
        <h4>View</h4>
            <ul>
                <li>Welcome Article</li>
                <li>Aside Navbar</li>
                <ul>
                    <li>AJouter une r&eacute;sidence</li>
                    <li>Liste de r&eacute;sidences</li>
                    <li>Liste de materiel</li>
                    <li>&Eacute;fficat&eacute;</li>
                </ul>
            </ul>
        <h4>Controller</h4>
            <ul>
                <li>call getListResidence()</li>
                <li>call getHourlyEfficiency()</li>
                <li>go to page "AJouter une r&eacute;sidence"(residence[])</li>
                <li>go to page "Liste de r&eacute;sidences"</li>
                <li>go to page "&Eacute;fficat&eacute;"(Semester[])</li>
            </ul>
        <h4>Back End</h4>
            <h5>Database<h5>
                <ul>
                    <li>Create database solar/solar</li>
                    <li>Create table residence(id,adress)</li>
                    <li>Create table residence_device(id_residence,device,power,start_hour,end_hour)</li>
                    <li>Create table semester(id,name,start_date,start_date)</li>
                    <li>Create table hourly_efficiency(id,id_semester,start_hour,end_hour,percentile_efficiency)</li>
                    <li>Create table solar_panel(id,name,price_per_watt)</li>
                    <li>Create table battery(id,name,price_per_watt,a_plat)</li>
                    <li>Create table residence_consumption(id,id_residence,id_semester,solar_panel_power,battery_charge)</li>
                    <li>Create table bill(id,id_residence,id_semester,id_solar_panel,solar_panel_price,id_battery,battery_price,total_price)</li>
                </ul>
            <h5>classes</h5>
                <ul>
                    <li>Residence(id,adress,device[])</li>
                    <li>Device(id_residence,device,power,startHour,endHour)</li>
                    <li>Semester(id,name,startDate,endDate,hourlyEfficiency[])</li>
                    <li>HourlyEfficiency(id,startDate,endDate,percentileEfficiency)</li>
                    <li>SolarPanel(id,name,pricePerWatt)</li>                    
                    <li>Battery(id,name,pricePerWatt,aPlat)</li>
                    <li>PSQLConn</li>
                    <li>Util</li>
                    <ul>
                        <li>getListResidence()</li>
                        <li>getListDevice()</li>
                        <li>getListHourlyEfficiency()</li>
                        <li>getListSemester()</li>
                    </ul>
                </ul>

<h2>Guide & Tips:</h2>
<h3>Install Npgsql</h3>
<ul>
    <li>nuget install Npgsql</li>
    <li>dotnet restore</li>
</ul>

