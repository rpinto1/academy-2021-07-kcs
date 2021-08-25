
import React, { useState } from 'react';
import axios from 'axios';
import { Button, Form, Checkbox, Container } from 'semantic-ui-react';

export default function SignUpForm() {

    const [newUser, setNewUser] = useState({
        FirstName: '',
        LastName: '',
        Password: '',
        ConfirmPassword: '',
        EmailAddress:''          
    });

    const [isRobot, setIsRobot] = useState('');


  /*  const validatePasswords = () => {
        if (newUser.password !== confirmPassword){
            return (
                <div class="ui negative message">
                    <i class="close icon"></i>
                        <div class="header">
                            Your passwords don't match.
                        </div>
                        <p>Please re-enter the passwords</p>
                        </div>
            )
        }
        return (
            <div class="ui success message">
            <i class="close icon"></i>
                <div class="header">
                    Your user registration was successful.
                </div>
            <p>You may now log-in with your email address and password.</p>
            </div>
        )
    }; */

    const submitUser = () => {

        axios.post('api/user', newUser)
             .catch ((error) => {console.log(error);});

        console.log(newUser);

        
    };



    return (
       
    <Container className= 'form'> 
         <h1> Create an account with us </h1>
        <Form onSubmit={submitUser}>
        <Form.Field>
            <label>First Name</label>
            <input 
            type= 'text' 
            placeholder='Write your First Name' 
            value={newUser.FirstName} 
            onChange={({ target }) => setNewUser((prevState)=> ({...prevState, FirstName: target.value,}))}
            pattern="[\w+/\s*]{3,50}" 
            required/>
        </Form.Field>
        <Form.Field>
            <label>Last Name</label>
            <input 
            type= 'text' 
            placeholder='Write your Last Name' 
            value={newUser.LastName} 
            onChange={({ target }) => setNewUser((prevState)=> ({...prevState, LastName: target.value,}))}
            pattern="[\w+/\s*]{3,50}"  
            required/>
        </Form.Field>
        <Form.Field>
            <label>Password</label>
            <input
            type= 'password' 
            placeholder='Create your password' 
            value = {newUser.Password}
            onChange= {({ target }) => setNewUser((prevState)=> ({...prevState, Password: target.value,}))}
            pattern='(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,100}' 
            title="Must contain at least one number, one uppercase, one lowercase letter; and at least 8 characters."
            required/>
        </Form.Field>
        <Form.Field>
            <label>Repeat password</label>
            <input 
            type ='password'
            placeholder='Rewrite your password' 
            value= {newUser.ConfirmPassword}
            onChange= {({target}) => setNewUser((prevState)=> ({...prevState, ConfirmPassword: target.value,}))}
            required/>
        </Form.Field>
        <Form.Field>
            <label>E-mail</label>
            <input 
            type= 'email' 
            placeholder='Write your e-mail address' 
            value = {newUser.EmailAdress} 
            onChange={({ target }) => setNewUser((prevState)=> ({...prevState, EmailAddress: target.value,}))}
            required/>
        </Form.Field>
        <Form.Field>
            <Checkbox type='checkbox' 
            name='isRobot' 
            label='Are you a robot?' 
            onChange= {({ target }) => setIsRobot('false')}
            required/>        </Form.Field>
        <Button type='submit'>Submit</Button>
        </Form>

        </Container>
        
        
    ) 

   
}
