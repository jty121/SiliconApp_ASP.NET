// JS FÖR DARK-LIGHT MODE
document.addEventListener('DOMContentLoaded', function () {
    let sw = document.querySelector('#switch-mode')

    sw.addEventListener('change', function () {
        let theme = this.checked ? "dark" : "light" //om checkboxen är i bockad så sätt antingen mörkt eller ljust tema

        //ajax anrop för att köra en action på en controller till en javascript fil, för att sidan ska köras utan att ladda om sig
        fetch(`/settings/changetheme?theme=${theme}`)
            .then(res => {
                if (res.ok) 
                    window.location.reload() //kommer åt våra endpoints i våra controllers via fetch.
                else
                    console.log('something went wrong in this function...')
            })
    })
})



// JS FÖR BYTE AV PROFIL BILD, SUBMIT AV FORMULÄR MED HJÄLP AV JS
document.addEventListener('DOMContentLoaded', function () {
    handleProfileImageUpload()
})

function handleProfileImageUpload() {
    try {
        let fileUploader = document.querySelector('#fileUploader')

        if (fileUploader != undefined) {

            fileUploader.addEventListener('change', function () {
                if (this.files.length > 0) //om filen som skickas innehåller en eller flera filer
                    this.form.submit() // då submittas formuläret
            })
        }
    }
    catch { }
}



// JS FÖR CURTAIN MENU PÅ HOME SIDAN
function openNav() {
    document.getElementById("myNav").style.height = "100%"
}

function closeNav() {
    document.getElementById("myNav").style.height = "0%"
}



// JS FÖR ATT FIXA BAKGRUNDSFÄRGEN PÅ HEADERN - CONTACT SIDAN
document.addEventListener("DOMContentLoaded", function () {
    var header = document.querySelector('Header');
    if (document.title === 'Contact us - Silicon') {
        header.style.backgroundColor = '#F3F6FF';
    }
});



// JS FÖR ATT FÅ TILL EN TOOLTIP på SAVED COURSES - från bootstraps exempel
var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl)
})
