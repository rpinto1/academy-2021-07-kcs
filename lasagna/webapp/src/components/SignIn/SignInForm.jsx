import React, { useEffect, useState } from 'react';
import { Redirect, Link } from 'react-router-dom';
import { Button, Checkbox, Container, Form } from 'semantic-ui-react';
import FailedSignUp from '../SignUp/FailedSignUp';


export default function SignInForm() {

    const [user, setUser] = useState({
      EmailAddress: '',
      Password: '', 
    });

    const [loggedUser, setLoggedUser] = useState({
      id: '',
      token: ''
    });

   //if all went well, the user is redirected to the user homepage
   const [redirect, setRedirect] = useState(false);
   //if user wishes to remain logged, token and id will be saved no localstorage, if not, on sessionstorage
   const [keepMeLogged, setKeepMeLogged] = useState(false);
   //if the password on the input is not correct, user will receive this message
   const [incorrectPassword, setIncorrectPassword] = useState(false);
   //if there are issues with the DB, user will receive this message
   const [dBError, setDBError] = useState(false);

    const handleChange = (event) => {
      const { id, value } = event.target
      
      setUser(prevState => ({
          ...prevState,
          [id]: value     
      }));
      };
     
  const handleKeepMeLogged = (event) => {
      setKeepMeLogged(!keepMeLogged);
  };
    
  useEffect (( ) => {
    if(keepMeLogged) {
      localStorage.setItem('id', loggedUser.id.toString());
      localStorage.setItem('token', loggedUser.token.toString());
    } else {
      sessionStorage.setItem('id', loggedUser.id.toString());
      sessionStorage.setItem('token', loggedUser.token.toString());
    }
  }, [keepMeLogged]); 
    

  const handleSubmit = () => {
      fetch(`http://localhost:3010/api/SignIn`, {
      method: 'POST',
      headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
      },
      body: JSON.stringify(user)
     }).then(res => res.json())
      .then(response => {
        if(response.status === 400) {
          setIncorrectPassword(true);
      } if (response.status === 404){
          setDBError(true);
      } if (response.status === 200) {
        setLoggedUser(response.result)
        setRedirect(true)
      }     
    })
    .catch(error => console.log(error))
    
};  

console.log(user);
console.log(loggedUser);

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
              <Link to ='/forgottenpassword'><p>I don't remember my password.</p></Link>
            </Form.Field>
            <Form.Field>
              <Checkbox label='Keep me logged in!' onClick ={handleKeepMeLogged} />
            </Form.Field>
              <Button type="submit" id="submit_btn" >Sign in</Button>
            </Form>
        
        { redirect &&
            <Redirect to='/user/homepage' />
        }

        </Container>

        
      );
    
    

};