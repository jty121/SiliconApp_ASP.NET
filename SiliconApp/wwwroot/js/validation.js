const formErrorValidate = (element, validateResult) => { //error message för formulär, lägga till och ta bort text i realtid
    // här hämtas elementet "span" upp
    let spanElement = document.querySelector(`[data-valmsg-for="${element.name}"]`)

    if (validateResult) { //om valideringen är giltig vill vi ta bort klasser som läggs på med vårat felmeddelande
        element.classList.remove('input-validation-error')
        spanElement.classList.remove('field-validation-error')
        spanElement.classList.add('field-validation-valid') //klassen som läggs till när något är giltigt vill vi lägga till
        spanElement.innerHTML = "" //tar bort texten ovanför 
    }
    else {
        element.classList.add('input-validation-error')
        spanElement.classList.add('field-validation-error')
        spanElement.classList.remove('field-validation-valid')
        spanElement.innerHTML = element.dataset.valRequired //lägg till texten igen
    }
}

const lengthValidate = (value, minLength = 2) => { //validerar längd, bra att stava rätt om det ska funka..length inte lenght!
    if (value.length >= minLength) {
        return true
    }
    else {
        return false
    }
}

const compareValidate = (value, compareValue) => { //jämför värden
    if (value === compareValue) {
        return true
    }
    else {
        return false
    }
}

const checkedValidate = (element) => { //kontrollerar om något är ifyllt/ibockat
    if (element.checked) {
        return true
    }
    else {
        return false
    }
}

const textValidate = (element) => {
    formErrorValidate(element, lengthValidate(element.value)) //använder sig av funktionen lenghtValidate
}

const checkboxValidate = (element) => {
    formErrorValidate(element, checkedValidate(element)) //använder sig av funktionen checkedvalidate
}

const emailValidate = (element) => {
    const regEx = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/
    formErrorValidate(element, regEx.test(element.value))
}

const passwordValidate = (element) => {
    if (element.dataset.valEqualtoOther !== undefined) {
        let password = document.getElementsByName(element.dataset.valEqualtoOther.replace('*', 'Form'))[0].value

        if (element.value === password)
            formErrorValidate(element, true)
        else
            formErrorValidate(element, false)
    }
    else {
        const regEx = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/
        formErrorValidate(element, regEx.test(element.value))
    }
}

let forms = document.querySelectorAll('form')
let inputs = forms[0].querySelectorAll('input')

inputs.forEach(input => {

    if (input.dataset.val === 'true') {

        if (input.type === 'checkbox') {
            input.addEventListener('change', (e) => {
                checkboxValidate(e.target)
            })
        }
        else {
            input.addEventListener('keyup', (e) => { //lyssnar på när en knapp släpps, då triggar valideringen igång.

                switch (e.target.type) {
                    case 'text':
                        textValidate(e.target)
                        break;
                    case 'email':
                        emailValidate(e.target)
                        break;
                    case 'password':
                        passwordValidate(e.target)
                        break;
                }
            })
        }
    }
})
