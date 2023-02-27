/* 
   Copyright (C) Damien Golding (dgolding, 2020-1-17 - 13:49)
   
   This is free software; you can redistribute it and/or
   modify it under the terms of the GNU Library General Public
   License as published by the Free Software Foundation; either
   version 2 of the License, or (at your option) any later version.
   
   This library is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
   Library General Public License for more details.
   
   You should have received a copy of the GNU Library General Public
   License along with this software; if not, write to the Free
   Software Foundation, Inc., 59 Temple Place - Suite 330, Boston,
   MA 02111-1307, USA

  Don't use it to find and eat babies ... unless you're really REALLY hungry ;-)

*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldingsGym.ParserLib {
    public class OptionsParser {
        string[] _args;
        public string[] Arguments {
            get {
                return (_args);
            }
            set {
                _args = value;
            }
        }
        bool _verbose = false;
        public bool Verbose {
            get {
                return (_verbose);
            }
        }
        bool _help = false;
        public bool Help {
            get {
                return (_help);
            }
        }
        bool _version = false;
        public bool Version {
            get {
                return (_version);
            }
        }
        bool _error = false;
        public bool Error {
            get {
                return (_error);
            }
        }
        string _fBA = String.Empty;
        public string FirstBadArgument {
            get {
                return (_fBA);
            }
        }
        Hashtable _ht = new Hashtable();
        public OptionsParser(string[] args) {
            _args = args;
            ParseArgs();
        }
        public void ParseArgs() {
            try {
                if (_args.Length == 0) {
                    return;
                }
                foreach (string a in _args) {
                    string tStr = String.Empty;
                    if (a.Contains('=')) {
                        tStr = a;
                        while (tStr.StartsWith("-")) {
                            tStr = a.TrimStart('-');
                        }
                        try {
                            string k = tStr.Split('=')[0].ToLower();
                            string v = tStr.Split('=')[1];
                            _ht.Add(k, v);
                        }
                        catch (Exception ex) {
                            _error = true;
                            _fBA = a;
                            return;
                        }
                    }
                    else {
                        if (a.ToLower().Equals("--help") || a.Equals("-h")) {
                            _help = true;
                        }
                        else if (a.ToLower().Equals("--verbose") || a.Equals("-v")) {
                            _verbose = true;
                        }
                        else if (a.ToLower().Equals("--version") || a.Equals("-V")) {
                            _version = true;
                        }
                        else {
                            _error = true;
                            _fBA = a;
                        }
                    }
                }
            }
            catch (Exception ex) {
                Console.Write("[-] Exception caught in parse_args: {0}", ex.Message);
            }
        }
        public string GetOpt(string key, string def = null) {
            string ret = def;
            foreach (DictionaryEntry de in _ht) {
                if (de.Key.ToString().Equals(key.ToLower())) {
                    return (de.Value.ToString());
                }
            }
            return (ret);
        }
        public bool HasOpt(string key) {
            bool ret = false;
            foreach (DictionaryEntry de in _ht) {
                if (de.Key.ToString().ToLower().Trim().Equals(key.ToLower().Trim())) {
                    return (true);
                }
            }
            return (ret);
        }
    }
}
