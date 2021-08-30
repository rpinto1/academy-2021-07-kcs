import React, {useState} from 'react';
import { Button, Container, Form } from 'semantic-ui-react';
import axios from 'axios';
import Captcha from '../SignUp/Captcha';
import { validateCaptcha } from 'react-simple-captcha';

export default function SignInForm() {

    const [user, setUser] = useState({
      EmailAddress: '',
      Password: ''
    });

    const handleChange = (event) => {
      const { id, value } = event.target
      
      setUser(prevState => ({
          ...prevState,
          [id]: value     
      }));
      
  };
    

    const handleSubmit = () => {
        
      let user_captcha = document.getElementById('user_captcha_input').value;

      if (validateCaptcha(user_captcha)==true) {
          console.log(user);
          /* axios.post('api/user/authenticate', user)
           .catch ((error) => {console.log(error);});
        
          //recharge captcha box
          loadCaptchaEnginge(6); 
          document.getElementById('user_captcha_input').value = ""; */
      }

      else {
          alert('Oops! Our page is only available to people, not robots!');
          document.getElementById('user_captcha_input').value = "";
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
         </Form.Field>
          <Button type="submit" id="submit_btn">Sign in</Button>
        </Form>
        </Container>

      );
    
    

};
