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
                <li>(ok)Welcome Article</li>
                <li>(ok)Aside Navbar</li>
                <ul>
                    <li>(ok)AJouter une r&eacute;sidence</li>
                    <li>(ok)Liste de r&eacute;sidences</li>
                    <li>(ok)Liste de materiel</li>
                    <li>(ok)&Eacute;fficat&eacute;</li>
                </ul>
            </ul>
        <h4>Controller</h4>
            <ul>
                <li>(ok)call getListResidence()</li>
                <li>(ok)call getHourlyEfficiency()</li>
                <li>(ok)go to page "AJouter une r&eacute;sidence"(residence[])</li>
                <li>(ok)go to page "Liste de r&eacute;sidences"</li>
                <li>(ok)go to page "&Eacute;fficat&eacute;"(Semester[])</li>
            </ul>
        <h4>Back End</h4>
            <h5>Database<h5>
                <ul>
                    <li>(ok)Create database solar/solar</li>
                    <li>(ok)Create table residence(id,adress)</li>
                    <li>(ok)Create table residence_device(id_residence,device,power,start_hour,end_hour)</li>
                    <li>(ok)Create table semester(id,name,start_date,start_date)</li>
                    <li>(ok)Create table hourly_efficiency(id,id_semester,start_hour,end_hour,percentile_efficiency)</li>
                    <li>(ok)Create table solar_panel(id,name,price_per_watt)</li>
                    <li>(ok)Create table battery(id,name,price_per_watt,a_plat)</li>
                    <li>(ok)Create table residence_consumption(id,id_residence,id_semester,solar_panel_power,battery_charge)</li>
                    <li>(ok)Create table bill(id,id_residence,id_semester,id_solar_panel,solar_panel_price,id_battery,battery_price,total_price)</li>
                </ul>
            <h5>classes</h5>
                <ul>
                    <li>(ok)Residence(id,adress,device[])</li>
                    <li>(ok)Device(id_residence,device,power,startHour,endHour)</li>
                    <li>(ok)Semester(id,name,startDate,endDate,hourlyEfficiency[])</li>
                    <li>(ok)HourlyEfficiency(id,startDate,endDate,percentileEfficiency)</li>
                    <li>(ok)SolarPanel(id,name,pricePerWatt)</li>                    
                    <li>(ok)Battery(id,name,pricePerWatt,aPlat)</li>
                    <li>(ok)PSQLConn</li>
                    <li>(ok)Util</li>
                    <ul>
                        <li>getListResidence()</li>
                        <li>getListDevice()</li>
                        <li>(ok)getListHourlyEfficiency()</li>
                        <li>(ok)getListSemester()</li>
                    </ul>
                </ul>

<h2>Guide & Tips:</h2>
<h3>Install Npgsql</h3>
<ul>
    <li>get nuget.exe</li>
    <li>nuget install Npgsql</li>
    <li>dotnet restore</li>
</ul>

