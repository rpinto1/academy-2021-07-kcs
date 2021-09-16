import React, { useEffect, useState } from 'react';

export default function RecoverForm({user}) {

    const [newUser, setNewUser] = useState({
        FirstName: '',
        LastName: '',
        EmailAddress:'',
        Password: '',
        ConfirmPassword: ''    
    });

    return (
        <div>
            Hello
        </div>
    )
}
