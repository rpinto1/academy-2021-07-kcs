import React, { useState, useEffect, useRef } from 'react';
import { Form, Container, Button } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import Footer from '../Footer';
import Captcha from './Captcha';
import FailedSignUp from './FailedSignUp';
import SuccessfulSignUp from './SuccessfulSignUp';
import { validateCaptcha, loadCaptchaEnginge } from 'react-simple-captcha';


export default function SignUpForm() {

    const [newUser, setNewUser] = useState({
        FirstName: '',
        LastName: '',
        EmailAddress:'',
        Password: '',
        ConfirmPassword: ''    
    });

    const captchaInputRef = useRef('');

    const handleChange = (event) => {
        const { id, value } = event.target 
        setNewUser(prevState => ({
            ...prevState,
            [id]: value     
        }));
    };

    const [accountAlreadyExists, setAccountAlreadyExists] = useState(false);
    const [successfullSignUp, setSuccessfullSignUp] = useState(false);
    const [dBError, setDBError] = useState(false);


    const handleSubmit = () => {
        let user_captcha = captchaInputRef.current.value;
  
        if (validateCaptcha(user_captcha) == true) {

        fetch(`http://localhost:3010/api/SignUp`, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newUser)
        }).then(response => {
            if(response.status === 400) {
                setAccountAlreadyExists(true);
            } if (response.status === 404){
                setDBError(true);
            } if (response.status === 200) {
                setSuccessfullSignUp(true);
            }
        })
       
        .catch(error => console.log(error))
          
        }  else {
        alert('Captcha Does Not Match');
        loadCaptchaEnginge(6); 
        }
    };
    

    
   

    return (
    <>   
    {
            successfullSignUp && <SuccessfulSignUp id='floating-msg' />
    }
    <Container className= 'formulario'> 
         <h1> Create an account with us </h1>
        <Form onSubmit={handleSubmit}>
        <Form.Field>
            <label>First Name</label>
            <input 
            type= 'text' 
            placeholder='Write your First Name' 
            value={newUser.FirstName} 
            onChange={handleChange}
            id='FirstName'
            pattern="[\w+/\s*]{2,15}"      
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
            pattern="[\w+/\s*]{2,15}"
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
            pattern='(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,50}' 
            title="Passwords must contain at least 8 characters. At least one number, one uppercase, one lowercase letter and one special character."
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
          <div className="container">
               <div className="form-group">
                   <div></div>
                   <div className="col mt-3">
                        <input 
                         placeholder="Enter Captcha Value" 
                         ref={captchaInputRef}>
                        </input>
                    </div>
                </div>
            </div> 
    
        </Form.Field>
        <Form.Field>
            <Link to ='/forgottenpassword'><p>I don't remember my password.</p></Link>
        </Form.Field>
        <Form.Field>
            <Button type='submit'  className='ui small center teal button'>Submit</Button>
            <Link to= '/'><Button type='cancel' className='ui small center red button'>Cancel</Button></Link>
        </Form.Field>
        
        </Form>
       
        {
           dBError && <FailedSignUp /> 
        }

        





        </Container>
        <Footer />
        </>
    ) 

   
}

