import React, { useEffect, useState } from 'react';
import { Redirect, Link } from 'react-router-dom';
import { Button, Checkbox, Container, Form } from 'semantic-ui-react';



export default function SignInForm() {

    const [user, setUser] = useState({
        Email: '',
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
    
  const saveUser = () => {
    console.log(loggedUser.id)
    if(keepMeLogged) {
      localStorage.setItem('id', loggedUser.id.toString());
      localStorage.setItem('token', loggedUser.token.toString());
      return;
    } 
      sessionStorage.setItem('id', loggedUser.id.toString());
      sessionStorage.setItem('token', loggedUser.token.toString());
  };

  const handleSubmit = () => {
      fetch(`http://localhost:3010/api/SignIn`, {
      method: 'POST',
      headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
      },
      body: JSON.stringify(user)
     }).then(res =>{
       console.log(res)
       console.log(res.headers.get('Set-Cookie'))
      res.json()
     } )
     .then(data => {
       console.log(data)
      setLoggedUser(data.response)
      setRedirect(true)
    })
    .catch(error => console.log(error))
    
};  

    useEffect(() =>{
      saveUser()
    },[loggedUser]);

console.log(user);
console.log(loggedUser);

      return (
        <Container className= 'formulario'>
            <h1>Sign in to your account</h1>
            <Form onSubmit={handleSubmit}>
                <Form.Field>
                    <label htmlFor="username">Username: </label>
                    <input
                        type="text"
                        value={user.Email}
                        placeholder="Enter your email address here"
                        id="Email"
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
                    <Link to='/forgottenpassword'><p>I don't remember my password.</p></Link>
                </Form.Field>
                <Form.Field>
                    <Checkbox label='Keep me logged in!' onClick={handleKeepMeLogged} />
                </Form.Field>
                <Button type="submit" id="submit_btn" >Sign in</Button>
            </Form>

            {/* { redirect &&
            <Redirect to='/user/homepage' />
        } */}

        </Container>


    );



};