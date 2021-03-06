/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */


package OpenSource.opennlp;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;

import java.io.IOException;

import opennlp.tools.ml.model.EventStream;
import opennlp.tools.util.ObjectStream;
import opennlp.tools.util.ObjectStreamUtils;

import org.junit.Test;

/**
 * Tests for the {@link TokSpanEventStream} class.
 */
public class TokSpanEventStreamTest {

  /**
   * Tests the event stream for correctly generated outcomes.
   */
  @Test
  public void testEventOutcomes() throws IOException {
    
    ObjectStream<String> sentenceStream = 
      ObjectStreamUtils.createObjectStream("\"<SPLIT>out<SPLIT>.<SPLIT>\"");
  
    ObjectStream<TokenSample> tokenSampleStream = new TokenSampleStream(sentenceStream);
    
    EventStream eventStream = new TokSpanEventStream(tokenSampleStream, false);
    
    assertTrue(eventStream.hasNext());
    assertEquals(TokenizerME.SPLIT, eventStream.next().getOutcome());
    assertTrue(eventStream.hasNext());
    assertTrue(eventStream.hasNext());
    assertEquals(TokenizerME.NO_SPLIT, eventStream.next().getOutcome());
    assertTrue(eventStream.hasNext());
    assertEquals(TokenizerME.NO_SPLIT, eventStream.next().getOutcome());
    assertTrue(eventStream.hasNext());
    assertEquals(TokenizerME.SPLIT, eventStream.next().getOutcome());
    assertTrue(eventStream.hasNext());
    assertEquals(TokenizerME.SPLIT, eventStream.next().getOutcome());
    
    assertFalse(eventStream.hasNext());
  }
}
