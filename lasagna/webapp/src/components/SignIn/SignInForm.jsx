import React, { useState, useRef } from 'react';
import { Redirect } from 'react-router-dom';
import { Button, Container, Form } from 'semantic-ui-react';
import Captcha from '../SignUp/Captcha';
import { validateCaptcha, loadCaptchaEnginge } from 'react-simple-captcha';


export default function SignInForm() {

    const [user, setUser] = useState({
      EmailAddress: '',
      Password: ''
    });

 
    const captchaInputRef = useRef('');

    const [captchaPass, setCaptchaPass] = useState(false);

    const [loggedUser, setLoggedUser] = useState({
      id: '',
      token: ''
    })

    const handleChange = (event) => {
      const { id, value } = event.target
      
      setUser(prevState => ({
          ...prevState,
          [id]: value     
      }));
      
  };
    
    const handleSubmit = () => {

      let user_captcha = captchaInputRef.current.value;

      if (validateCaptcha(user_captcha) == true) {
        
        fetch(`http://localhost:3010/api/SignIn`, {
          method: 'POST',
          headers: {
              'Accept': 'application/json',
              'Content-Type': 'application/json'
          },
          body: JSON.stringify(user)
        }).then(res => res.json())
          .then(data => {
           setLoggedUser(data.result);
          console.log("Logged user id is: " + loggedUser.id +'. Logged user token is ' + loggedUser.token)
          //setCaptchaPass(true);

         // if (captchaPass) {<Redirect to='/user/homepage' loggedUser={loggedUser}/>};

        })

      .catch(error => console.log(error));

      } else {
          alert('Captcha Does Not Match');
          loadCaptchaEnginge(6); 
      }
  };  
  

    
      return (
        <Container className= 'formulario'>
          <h1>Sign in to your account</h1>
          <Form onSubmit= {handleSubmit}>
         <Form.Field> 
          <label htmlFor="username">Username: </label>
          <input
            type="text"
            value={user.EmailAddress}
            placeholder="Enter your email address here"
            id="EmailAddress"
            onChange={handleChange}
          />
          </Form.Field>  

          <Form.Field> 
            <label htmlFor="password">Password: </label>
            <input
              type="password"
              value={user.Password}
              placeholder="Enter your password here"
              id="Password"
              onChange={handleChange}
            />
         </Form.Field> 
         <Form.Field>
         <Captcha />
          <div className="col mt-3">
                   <div></div>
                       <div>   
                           <input 
                           placeholder="Enter Captcha Value" 
                           name="user_captcha_input" 
                           type="text"
                           ref={captchaInputRef}>
                            </input>
                        </div>
                   </div>
         </Form.Field>
          <Button 
          type="submit" 
          id="submit_btn">Sign in</Button>
        </Form>
        </Container>

      );
    
    

};