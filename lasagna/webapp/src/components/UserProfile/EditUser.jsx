import React, {useEffect, useState} from 'react';
import { Form, Container, Button } from 'semantic-ui-react';
import { Link } from 'react-router-dom';

import { token, urlGetUser, urlUpdateUser } from '../UserManager';
 

export default function EditUser() {

    //all updateable except for emailaddress.
    const [dbUserInfo, setDBUserInfo] = useState({
        OldPassword: '',
        NewPassword: '',
        ConfirmNewPassword: ''
    });

    const [userName, setUserName] = useState('');

    const [passwordsMatch, setPasswordsMatch] = useState({
        match: false,
        showMessage: false
    });

    useEffect(() =>
        fetch(urlGetUser, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        }
    }).then(res => res.json())
       .then(data => {
        setUserName(data.result.firstName)
        setDBUserInfo(prevState => ({
            ...prevState,
            Id: data.result.id,
            FirstName: data.result.firstName,
            LastName: data.result.lastName,
            Email: data.result.email
        }))
      })
      .catch(error => console.log(error)), []);

      

    const handleEditUpdate = () => {
        if (passwordsMatch){
        fetch(urlUpdateUser, {
        method: 'PUT',
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"

        },
        body: JSON.stringify(dbUserInfo)
       }).then(res => res.json())
      .catch(error => console.log(error))
    }
  };

    const handleChange = (event) => {
        const { id, value } = event.target 
        setDBUserInfo(prevState => ({
            ...prevState,
            [id]: value     
        }));
    };

    const verifyPasswords = () => {
        if(dbUserInfo.NewPassword === dbUserInfo.ConfirmNewPassword){
            setPasswordsMatch({showMessage: false, match: true});
        } else {
            setPasswordsMatch(prevState => ({...prevState, showMessage: true}));
        }     
    };

    console.log(dbUserInfo)

    return (
        <Container className= 'formulario'> 
        <h1> {userName}, let's update your account </h1>
       <Form onSubmit={handleEditUpdate}> 
       <Form.Field>
           <label>First Name</label>
           <input 
           type= 'text' 
           placeholder={dbUserInfo.FirstName}
           value={dbUserInfo.FirstName}
           onChange = {handleChange}
           id='FirstName'/>
       </Form.Field>
         <Form.Field>
           <label>Last Name</label>
           <input 
           type= 'text' 
           placeholder={dbUserInfo.LastName}
           value={dbUserInfo.LastName}
           onChange = {handleChange}
           id='LastName'/>
       </Form.Field>
       <Form.Field>
           <label>Password <span className='tiny'>- Required</span></label>
           <input
           type= 'password' 
           placeholder='Input your current password'
           value={dbUserInfo.OldPassword} 
           onChange = {handleChange}
           id='OldPassword'
           pattern='(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,50}' 
           required/>
       </Form.Field>
       <Form.Field>
           <label>New password <span className='tiny'>- Optional</span></label>      
           <input 
           type ='password'
           placeholder='Write your new password' 
           onChange={handleChange}
           value = {dbUserInfo.NewPassword}
           id='NewPassword'/>
       </Form.Field>
       <Form.Field>
           <label>Repeat new password <span className='tiny'> - Not required unless updating your password</span></label>
           <input 
           type ='password'
           placeholder='Rewrite your password' 
           value = {dbUserInfo.ConfirmPassword}
           onChange={handleChange}
           onBlur={verifyPasswords}
           id='ConfirmPassword'/>
       </Form.Field>
       {
            passwordsMatch.showMessage && 

            <div class="ui bottom attached negative message">
            <i class="close icon"></i>
                Confirm password needs to match your new password!
            </div>
        }
       <Form.Field>
           <label>E-mail</label>
           <input 
           type= 'email' 
           placeholder='Write your old e-mail address' 
           value = {dbUserInfo.Email}
           readOnly
           id='EmailAddress'
           required/>
       </Form.Field>
       <Form.Field>
           <Button type='submit'  className='ui small center teal button'>Submit</Button>
           <Link to= '/'><Button type='cancel' className='ui small center red button'>Cancel</Button></Link>
       </Form.Field>
       
       </Form>
      

       </Container>
    )
}
