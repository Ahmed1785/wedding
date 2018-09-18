import Autosuggest from 'react-autosuggest';
import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import colors from './../colors';

const getSuggestions = value => {
    const inputValue = value.trim().toLowerCase();
    const inputLength = inputValue.length;

    return inputLength === 0 ? [] : colors.filter(lang =>
        lang.color.toLowerCase().slice(0, inputLength) === inputValue
    );
};

const getSuggestionValue = suggestion => suggestion.color;


const renderSuggestion = suggestion => ( 
    <div><Link to={suggestion.route}> {
        suggestion.color
    }
    </Link> 
    </div>
);
class Home extends Component {
    constructor() {
        super();

        this.state = {
            value: '',
            suggestions: []
        };
    }

    onChange = (event, {
        newValue
    }) => {
        this.setState({
            value: newValue
        });
    };

    onSuggestionsFetchRequested = ({
        value
    }) => {
        this.setState({
            suggestions: getSuggestions(value)
        });
    };

    onSuggestionsClearRequested = () => {
        this.setState({
            suggestions: []
        });
    };

  render() {
    const {
        value,
        suggestions
    } = this.state;

    const inputProps = {
        placeholder: 'Type here...',
        value,
        onChange: this.onChange
    };

    return (
    <div className="App">
      <h1>Auto Suggest Color Scheme</h1>

      <
            Autosuggest suggestions = {
                suggestions
            }
            onSuggestionsFetchRequested = {
                this.onSuggestionsFetchRequested
            }
            onSuggestionsClearRequested = {
                this.onSuggestionsClearRequested
            }
            getSuggestionValue = {
                getSuggestionValue
            }
            renderSuggestion = {
                renderSuggestion
            }
            inputProps = {
                inputProps
            }
            />
    </div>
    );
  }
}
export default Home;