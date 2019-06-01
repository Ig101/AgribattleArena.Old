export const ENVIRONMENT = {
    loadingTimeout: 10000,
    loadingSpeed: 0.1,

    defaultLoadingOpacity: 0.5,

    startLoadingTimeout: 5000,
    afterLoadingDelay: 500,

    queueTimeout: 240000,
};

export const STRINGS = {
    // ExternalResponseError
    queueTimeout: 'Reached queue timeout. Exiting queue.',
    notImplemented: 'Not implemented.',
    serverError: 'Server error. Try again later.',
    unathorized: 'Unauthorized. Return to the login page.',
    badRequest: 'Wrong input.',
    incorrectLogin: 'Incorrect login or password',
    authorizationFailed: 'Authorization failed. Returning to the main page...',
    unexpectedError: 'Unexpected error.',
    // ValidationError
    oneUppercaseError: 'should have at least one uppercase letter (A-Z).',
    oneSpecialError: 'should have at least one symbol from $,@,!,%,*,?,&.',
    oneLowercaseError: 'should have at least one lowercase letter (a-z).',
    oneDigitError: 'should have at least one digit.',
    emailError: 'should be valid.',
    requiredError: 'is required.',
    minLengthStartError: 'should contain at least ',
    maxLengthStartError: 'should contain less than or equal to ',
    lengthEndError: ' symbols.',
    letterDigitsError: 'should contain only letters (A-z) and digits.',
    firstLetterError: 'should start with letter (A-z).',
    confirmPasswordError: 'should be the same as password.',
};
