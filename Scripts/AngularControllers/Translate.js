app.config(function ($translateProvider) {

    // Simply register translation table as object hash
    $translateProvider.translations('en', {
        'HEADER': 'Header',
        'LOGIN': 'Login',
        'USERNAME': 'Username',
        'PASSWORD': 'Password',
        'CHANGE_LANGUAGE': 'Language changed',

        'MAIN_NAV': 'MAIN NAVIGATION',
        'LOGIN_MESSAGE': 'Wellcome',
        'MESSAGE_LABEL': 'Message:',
        'EMPTY_SUBHEADER': '',
        'DASHBOADR_LABEL': 'Dashboard',
        'TASK_LABEL': 'Tasks',
        'PROJECT_LABEL': 'Projects',
        'PROJECT_DETAIL_LABEL': 'Project details',
        'RESET_PASS_MESSAGE': 'New password is send on email!',
        'LINK_SENT': 'Link for reset is send on email!',
        'INVALID_LOGIN': 'Invalid username or password',
        '1': 'Invalid username or password',
        '2': 'Invalid username',
        'MODAL_HEADER_RESET_PASSWORD': 'Reset password'
    });

    $translateProvider.translations('en', {
        'REGISTER': 'Register'

    });

    $translateProvider.translations('bs', {
        'HEADER': 'Naslov',
        'LOGIN': 'Prijavi se',
        'REGISTER': 'Registruj se',
        'USERNAME': 'Korisnicko ime',
        'PASSWORD': 'Sifra',
        'CHANGE_LANGUAGE': 'Jezik promijenjen',

        'MAIN_NAV': 'GLAVNA NAVIGACIJA',
        'LOGIN_MESSAGE': 'Dobrodosli',
        'MESSAGE_LABEL': 'Poruka:',
        'EMPTY_SUBHEADER': '',
        'DASHBOADR_LABEL': 'Komandna tabla',
        'TASK_LABEL': 'Taskovi',
        'PROJECT_LABEL': 'Projekti',
        'PROJECT_DETAIL_LABEL': 'Detalji projekta',
        'RESET_PASS_MESSAGE': 'Nova sifra je poslana na mail!',
        'LINK_SENT': 'Link za reset je poslan na mail',
        'INVALID_LOGIN': 'Pogresano korisnicko ime ili sifra!',
        '1': 'Pogresano korisnicko ime ili sifra!',
        '2': 'Pogresano korisnicko ime!',
        'MODAL_HEADER_RESET_PASSWORD': 'Resetovanje sifre'
    });

    //$translateProvider.preferredLanguage($rootScope.currentLng);
});