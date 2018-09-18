import React, { Component } from 'react';
import { Route, Switch } from 'react-router-dom';
import './App.css';
import Home from './pages/Home';
import Red from './pages/Red';
import Orange from './pages/Orange';
import Yellow from './pages/Yellow';
import Green from './pages/Red';
import Blue from './pages/Blue';
import Indigo from './pages/Indigo';
import Violet from './pages/Violet';

class App extends Component {
  render() {
    const App = () => (
      <div>
        <Switch>
          <Route exact path='/' component={Home}/>
          
          <Route path='/red' component={Red}/>
          <Route path='/orange' component={Orange}/>
          <Route path='/yellow' component={Yellow}/>
          <Route path='/green' component={Green}/>
          <Route path='/blue' component={Blue}/>
          <Route path='/indigo' component={Indigo}/>
          <Route path='/violet' component={Violet}/>
        </Switch>
      </div>
    )
    return (
      <Switch>
        <App/>
      </Switch>
    );
  }
}

export default App;
