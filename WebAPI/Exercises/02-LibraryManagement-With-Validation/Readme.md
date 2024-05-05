# Exercise: Add Input Validation For the Library Management API

This exercise will add validation rules for the incoming POST and PUT requests for the Library Management API. We will introduce data annotations to appropriate data transfer objects and, where appropriate, add custom validation rules in addition to data annotations.

You may use your solution code from the previous exercise or start with our solution files in the **401\Exercises\02-LibraryManagement-With-Validation\Start\\** folder.

## Validation Rules for Borrowers

The borrower DTO should be modified to have the following validations:

- Add the **[Required]** attribute to all string properties. 
  - Ensure that **AllowEmptyStrings** is set to false.
  - Ensure the **ErrorMessage** is specified to make a nice, user-readable message.
- Add the **[EmailAddress]** attribute to the **Email** property. Again, use the **ErrorMessage** property to provide a good error message.
- Add the **[Phone]** attribute to the **Phone** property with an appropriate error message.

We will also implement the **IValidatableObject** interface and add some additional rules in the **Validate()** method:

- The **FirstName** and **LastName** properties should not allow white spaces or digits.

**Hint:** The **Any()** LINQ method makes this easy when combined with the **char.IsDigit()** and **char.IsWhiteSpace()** methods. See if you can figure it out!

## Add Validation Rules For Media

For the Media DTO, the only required field is the Title property. Do not allow empty strings.

## Add appropriate logging

Please add appropriate logging to capture the following:

- Successful requests as information
- Exceptions and 500 response codes as errors
- Non-error events that return bad request (400) codes as warnings

Both warning and error logs should include the original error messages returned from the services. The default built-in console logger is sufficient to meet this requirement.
