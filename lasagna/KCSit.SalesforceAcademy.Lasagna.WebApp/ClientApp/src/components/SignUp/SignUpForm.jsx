import React, { useState } from 'react';
import { Button, Form, Container } from 'semantic-ui-react';
import axios from 'axios';
import Captcha from './Captcha';

export default function SignUpForm() {

    const [newUser, setNewUser] = useState({
        FirstName: '',
        LastName: '',
        EmailAddress:'',
        Password: '',
        ConfirmPassWord: ''    
    });


    function handleChange (event) {
        const { id, value } = event.target
        
        setNewUser(prevState => ({
            ...prevState,
            [id]: value     
        }));
        
    };



    const handleSubmit = () => {
        
        let user_captcha = document.getElementById('user_captcha_input').value;
 
        if (validateCaptcha(user_captcha)==true) {
            
            axios.post('api/user', newUser)
             .catch ((error) => {console.log(error);});
             console.log(newUser);
            //recharge captcha box
            loadCaptchaEnginge(6); 
            document.getElementById('user_captcha_input').value = "";
        }
 
        else {
            alert('Captcha simbols must match!');
            document.getElementById('user_captcha_input').value = "";
        }
    };


    return (
       
    <Container className= 'form'> 
         <h1> Create an account with us </h1>
        <Form>
        <Form.Field>
            <label>First Name</label>
            <input 
            type= 'text' 
            placeholder='Write your First Name' 
            value={newUser.FirstName} 
            onChange={handleChange}
            id='FirstName'
            pattern="[\w+/\s*]{2,50}"     
            required/>
        </Form.Field>
        <Form.Field>
            <label>Last Name</label>
            <input 
            type= 'text' 
            placeholder='Write your Last Name' 
            value={newUser.LastName} 
            onChange={handleChange}
            id='LastName'
            pattern="[\w+/\s*]{2,50}"
            required/>
        </Form.Field>
        <Form.Field>
            <label>Password</label>
            <input
            type= 'password' 
            placeholder='Create your password' 
            value = {newUser.password}
            onChange={handleChange}
            id='Password'
            pattern='(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,100}' 
            title="Passwords must contain at least 8 characters and from those you need at least, one number, one uppercase and one lowercase letter."
            required/>
        </Form.Field>
        <Form.Field>
            <label>Repeat password</label>
            <input 
            type ='password'
            placeholder='Rewrite your password' 
            value= {newUser.ConfirmPassword}
            onChange={handleChange}
            id='ConfirmPassword'
            required/>
        </Form.Field>
        <Form.Field>
            <label>E-mail</label>
            <input 
            type= 'email' 
            placeholder='Write your e-mail address' 
            value = {newUser.EmailAddress} 
            onChange={handleChange}
            id='EmailAddress'
            required/>
        </Form.Field>
        <Form.Field>
            <Captcha />
        </Form.Field>
        <Button type='submit' onClick={() => handleSubmit}>Submit</Button>
        </Form>

        </Container>
        
        
    ) 

   
}
